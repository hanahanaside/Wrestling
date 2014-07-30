//
//  ImobileSdkAdsIosUnityPluginImpl.mm
//
//  Copyright 2014 i-mobile Co.Ltd. All rights reserved.
//

#import "ImobileSdkAdsIosUnityPluginImpl.h"
#import "ImobileSdkAds/ImobileSdkAdsIconParams.h"

#ifdef __cplusplus
extern "C" {
#endif
	void UnitySendMessage(const char* obj, const char* method, const char* msg);
#ifdef __cplusplus
}
#endif


@interface ImobileSdkAdsIosUnityPluginImpl ()

@end


@implementation ImobileSdkAdsIosUnityPluginImpl

static const NSMutableSet *gameObjectNames = [NSMutableSet set];
static const NSMutableDictionary *adViewIdDictionary = [NSMutableDictionary dictionary];

extern UIViewController *UnityGetGLViewController();

// ----------------------------------------
#pragma mark - Call from inner C++
// ----------------------------------------

- (void)addObserver:(const char*)gameObjectName {
    [gameObjectNames addObject:[NSString stringWithUTF8String:gameObjectName]];
}

- (void)removeObserver:(const char*)gameObjectName {
    [gameObjectNames removeObject:[NSString stringWithUTF8String:gameObjectName]];
}

- (void)registerWithPublisherID:(const char*)publisherid MediaID:(const char*)mediaid SoptID:(const char*)soptid {
    [ImobileSdkAds registerWithPublisherID:[NSString stringWithUTF8String:publisherid]
                                   MediaID:[NSString stringWithUTF8String:mediaid]
                                    SpotID:[NSString stringWithUTF8String:soptid]];
    
    [ImobileSdkAds setSpotDelegate:[NSString stringWithUTF8String:soptid] delegate:self];
}

- (void)start {
    [ImobileSdkAds start];
}

- (void)stop {
    [ImobileSdkAds stop];
}

- (bool)startBySpotID:(const char*)spotid {
    return [ImobileSdkAds startBySpotID:[NSString stringWithUTF8String:spotid]];
}

- (bool)stopBySpotID:(const char*)spotid {
    return [ImobileSdkAds stopBySpotID:[NSString stringWithUTF8String:spotid]];
}

- (bool)showBySpotID:(const char*)spotid {
    return [ImobileSdkAds showBySpotID:[NSString stringWithUTF8String:spotid]];
}

- (bool)showBySpotID:(const char*)spotid PublisherID:(const char*)publisherid MediaID:(const char*)mediaid Left:(int)left Top:(int)top Width:(int)width Height:(int)height iconNumber:(int)iconNumber iconViewLayoutWidth:(int)iconViewLayoutWidth iconTitleEnable:(bool)iconTitleEnable iconTitleFontColor:(const char*)iconTitleFontColor iconTitleShadowEnable:(bool)iconTitleShadowEnable iconTitleShadowColor:(const char*)iconTitleShadowColor iconTitleShadowDx:(int)iconTitleShadowDx iconTitleShadowDy:(int)iconTitleShadowDy adViewId:(int)adViewId {
    
    // create iconParams
    ImobileSdkAdsIconParams *params = [[ImobileSdkAdsIconParams alloc] init];
    params.iconNumber = iconNumber;
    params.iconViewLayoutWidth = iconViewLayoutWidth;
    params.iconTitleEnable = iconTitleEnable;
    params.iconTitleFontColor = [NSString stringWithUTF8String:iconTitleFontColor];
    params.iconTitleShadowEnable = iconTitleShadowEnable;
    params.iconTitleShadowColor = [NSString stringWithUTF8String:iconTitleShadowColor];
    params.iconTitleShadowDx = iconTitleShadowDx;
    params.iconTitleShadowDy = iconTitleShadowDy;
    
    // resister
    [self registerWithPublisherID:publisherid MediaID:mediaid SoptID:spotid];
    
    // start
    [self startBySpotID:spotid];
    
    // show
    UIView *adContainerView = [[UIView alloc] initWithFrame:CGRectMake(left, top, width, height)];
    [adViewIdDictionary setObject:adContainerView forKey:[NSString stringWithFormat:@"%d", adViewId]];
    [UnityGetGLViewController().view addSubview:adContainerView];
    
    return [ImobileSdkAds showBySpotID:[NSString stringWithUTF8String:spotid] View:adContainerView IconPrams:params];
}

- (void)setAdOrientation:(ImobileSdkAdsAdOrientation)orientation {
    [ImobileSdkAds setAdOrientation:orientation];
}

- (void)setAdView:(int)adViewId visible:(bool)visible {
    UIView *adContainerView = [adViewIdDictionary objectForKey:[NSString stringWithFormat:@"%d", adViewId]];
    adContainerView.hidden = !visible;
}

- (float)getScreenScale {
    return [UIScreen mainScreen].scale;
}

// ----------------------------------------
#pragma mark Call from ImobileSdkAds
// ----------------------------------------

- (void)imobileSdkAdsSpot:(NSString *)spotid didReadyWithValue:(ImobileSdkAdsReadyResult)value {
    NSString *msg = [NSString stringWithFormat:@"%@", spotid];
    
    for (NSString *gameObjectName in gameObjectNames ) {
        UnitySendMessage([gameObjectName UTF8String],
                         [@"imobileSdkAdsSpotDidReady" UTF8String],
                         [msg UTF8String]);
    }
}

- (void)imobileSdkAdsSpot:(NSString *)spotid didFailWithValue:(ImobileSdkAdsFailResult)value {
    NSString *msg = [NSString stringWithFormat:@"%@,%d", spotid,value];
    
    for (NSString *gameObjectName in gameObjectNames ) {
        UnitySendMessage([gameObjectName UTF8String],
                         [@"imobileSdkAdsSpotDidFail" UTF8String],
                         [msg UTF8String]);
    }
}

- (void)imobileSdkAdsSpotIsNotReady:(NSString *)spotid {
    NSString *msg = [NSString stringWithFormat:@"%@", spotid];
    
    for (NSString *gameObjectName in gameObjectNames ) {
        UnitySendMessage([gameObjectName UTF8String],
                         [@"imobileSdkAdsSpotIsNotReady" UTF8String],
                         [msg UTF8String]);
    }
}

- (void)imobileSdkAdsSpotDidClick:(NSString *)spotid {
    NSString *msg = [NSString stringWithFormat:@"%@", spotid];
    
    for (NSString *gameObjectName in gameObjectNames ) {
        UnitySendMessage([gameObjectName UTF8String],
                         [@"imobileSdkAdsSpotDidClick" UTF8String],
                         [msg UTF8String]);
    }
}

- (void)imobileSdkAdsSpotDidClose:(NSString *)spotid {
    NSString *msg = [NSString stringWithFormat:@"%@", spotid];
    
    for (NSString *gameObjectName in gameObjectNames ) {
        UnitySendMessage([gameObjectName UTF8String],
                         [@"imobileSdkAdsSpotDidClose" UTF8String],
                         [msg UTF8String]);
    }
}


// ----------------------------------------
#pragma mark - Call from Unity
// ----------------------------------------

#ifdef __cplusplus
extern "C" {
#endif
    static const ImobileSdkAdsIosUnityPluginImpl *unityPlugin = NULL;
    
    void imobileAddObserver_(const char* gameObjectName) {
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        [unityPlugin addObserver:gameObjectName];
    }
    
    void imobileRemoveObserver_(const char* gameObjectName) {
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        [unityPlugin removeObserver:gameObjectName];
    }
    
    void imobileRegisterWithPublisherID_(const char* publisherid, const char* mediaid, const char* soptid) {
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        [unityPlugin registerWithPublisherID:publisherid
                                     MediaID:mediaid
                                      SoptID:soptid];
    }
    
    void imobileStart_() {
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        [unityPlugin start];
    }
    
    void imobileStop_() {
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        [unityPlugin stop];
    }
    
    bool imobileStartBySpotID_(const char* spotid){
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        return [unityPlugin startBySpotID:spotid];
    }
    
    bool imobileStopBySpotID_(const char* spotid) {
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        return [unityPlugin stopBySpotID:spotid];
    }
    
    bool imobileShowBySpotID_(const char* spotid, int adViewId) {
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        return [unityPlugin showBySpotID:spotid];
    }
    
    bool imobileShowBySpotIDWithPositionAndIconParams_(const char* spotid,
                                                       const char* publisherid,
                                                       const char* mediaid,
                                                       int left,
                                                       int top,
                                                       int width,
                                                       int height,
                                                       int iconNumber,
                                                       int iconViewLayoutWidth,
                                                       bool iconTitleEnable,
                                                       const char* iconTitleFontColor,
                                                       bool iconTitleShadowEnable,
                                                       const char* iconTitleShadowColor,
                                                       int iconTitleShadowDx,
                                                       int iconTitleShadowDy,
                                                       int adViewId) {
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        return [unityPlugin showBySpotID:spotid
                             PublisherID:publisherid
                                 MediaID:mediaid
                                    Left:left
                                     Top:top
                                   Width:width
                                  Height:height
                              iconNumber:iconNumber
                     iconViewLayoutWidth:iconViewLayoutWidth
                         iconTitleEnable:iconTitleEnable
                      iconTitleFontColor:iconTitleFontColor
                   iconTitleShadowEnable:iconTitleShadowEnable
                    iconTitleShadowColor:iconTitleShadowColor
                       iconTitleShadowDx:iconTitleShadowDx
                       iconTitleShadowDy:iconTitleShadowDy
                                adViewId:adViewId];
    }
    
    void imobileSetAdOrientation_(ImobileSdkAdsAdOrientation orientation) {
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        [unityPlugin setAdOrientation:orientation];
    }
    
    void imobileSetVisibility_(int adViewId, bool visible) {
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        [unityPlugin setAdView:adViewId visible:visible];
    }
    
    float getScreenScale_() {
        if (!unityPlugin) {
            unityPlugin = [[ImobileSdkAdsIosUnityPluginImpl alloc] init];
        }
        return [unityPlugin getScreenScale];
    }
    
#ifdef __cplusplus
}
#endif

@end