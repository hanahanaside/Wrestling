//
//  GFUnityPlugin.h
//  GameFeatKit
//
//  Created by K.Yusa on 13/04/12.
//  Copyright (c) 2013îN Basicinc.jp. All rights reserved.
//

#import "GFUnityPlugin.h"

extern UIViewController* UnityGetGLViewController();

extern "C" {
    void activateGF_(const char* siteId, BOOL useCustom, BOOL useIcon, BOOL usePopup);
    void start_(const char* siteId);
    void startWtCallback_(const char* siteId, const char* objName);
    BOOL conversionCheck_();
    void show_(const char* siteId);
    void showWtCallback_(const char* siteId, const char* objName);
    void backgroundTask_();
    void conversionCheckStop_();
    //void initGFIconView_();
    
    GFIconController *gfIconController;
    void initGFIconController_();
    void addIconView_(int x, int y, int w, int h);
    void iconLoadAd_(const char* siteId);
    void iconStopAd_();
    void setIconAppNameColor_(float r, float g, float b);
    void setIconAdRefreshTiming_(int sec);
    void removeIconAd_();
    void invisibleIconAd_();
    void visibleIconAd_();
    
    GFPopupView *popupView;
    void initGFPopupView_();
    void initGFPopupViewCallback_(const char* objName);
    void setPopupAdSchedule_(int num);
    void setPopupAdAnimation_(bool flag);
    void popupLoadAd_(const char* siteId);
    
    NSString* charToString(const char*);
    UIViewController* getUnityViewController();
    void setApplicationNotification(GFUnityDelegate* callback);
}

#pragma mark -
#pragma mark GFView

// GFView.activateGF
void activateGF_(const char* siteId, BOOL useCustom, BOOL useIcon, BOOL usePopup){
    NSString* strSiteId = charToString(siteId);
    GFView *gfView = [[[GFView alloc] init] autorelease];
    [gfView activateGF:strSiteId useCustom:useCustom useIcon:useIcon usePopup:usePopup];
    
    GFUnityDelegate* callback = [[GFUnityDelegate alloc] init];
    setApplicationNotification(callback);
}

// GFView.start
void start_(const char* siteId) {
    NSLog(@"#start_");
    NSString* strSiteId = charToString(siteId);
    //    GFUnityDelegate* callback = [[GFUnityDelegate alloc] init];
    //    setApplicationNotification(callback);
    UIViewController* parent = getUnityViewController();
    [[GFView alloc] start:parent site_id:strSiteId];
}

// GFView.start delegate
void startWtCallback_(const char* siteId, const char* objName) {
    NSLog(@"#start_del");
    NSString* strSiteId = charToString(siteId);
    GFUnityDelegate* callback = [[GFUnityDelegate alloc] init];
    //    setApplicationNotification(callback);
    UIViewController* parent = getUnityViewController();
    NSString* strObjName = charToString(objName);
    [callback setObjectName:strObjName];
    [[GFView alloc] start:parent site_id:strSiteId delegate: callback];
}

// GFView.conversionCheck
BOOL conversionCheck_() {
    NSLog(@"#conversionCheckStop");
    return [[GFView alloc] conversionCheck];
}

#pragma mark -
#pragma mark GFController
// GFController.showGF
void show_(const char* siteId) {
    NSLog(@"#showGF");
    NSString* strSiteId = charToString(siteId);
    UIViewController* vc = getUnityViewController();
    [GFController showGF:vc site_id:strSiteId];
}

// GFController.showGF delegate
void showWtCallback_(const char* siteId, const char* objName) {
    NSLog(@"#showGF_del");
    NSString* strSiteId = charToString(siteId);
    UIViewController* vc = getUnityViewController();
    GFUnityDelegate* callback = [[GFUnityDelegate alloc] init];
    NSString* strObjName = charToString(objName);
    [callback setObjectName:strObjName];
    [GFController showGF:vc site_id:strSiteId delegate:callback];
}

// GFController.backgroundTask
void backgroundTask_() {
    NSLog(@"#backgroundTask");
    [GFController backgroundTask];
}

// GFController.conversionCheckStop
void conversionCheckStop_() {
    NSLog(@"#conversionCheckStop");
    [GFController conversionCheckStop];
}

// GFIconController.init
void initGFIconController_() {
    gfIconController = [[GFIconController alloc] init];
}

// GFIconController.addIconView
void addIconView_(int x, int y, int w, int h) {
    UIViewController* parent = getUnityViewController();
    
    GFIconView *iconView = [[[GFIconView alloc] initWithFrame:CGRectMake(x, y, w, h)] autorelease];
    [gfIconController addIconView:iconView];
    [parent.view addSubview:iconView];
}

void iconLoadAd_(const char* siteId) {
    NSString* strSiteId = charToString(siteId);
    [gfIconController loadAd:strSiteId];
    
    //    GFUnityDelegate* callback = [[GFUnityDelegate alloc] init];
    //    setApplicationNotification(callback);
}

void iconStopAd_() {
    [gfIconController stopAd];
}

void setIconAppNameColor_(float r, float g, float b) {
    [gfIconController setAppNameColor:[UIColor colorWithRed:r green:g blue:b alpha:1.0]];
}

void setIconAdRefreshTiming_(int sec) {
    [gfIconController setRefreshTiming:sec];
}

void removeIconAd_() {
    [gfIconController stopAd];
    UIViewController* parent = getUnityViewController();
    for(UIView *removeView in [parent.view subviews]){
        [removeView removeFromSuperview];
    }
}

void invisibleIconAd_() {
    [gfIconController invisibleIconAd];
}

void visibleIconAd_() {
    [gfIconController visibleIconAd];
}

void initGFPopupView_() {
    popupView = [[GFPopupView alloc] init];
}
void initGFPopupViewCallback_(const char* objName) {
    
    GFUnityDelegate* callback = [[GFUnityDelegate alloc] init];
    //    setApplicationNotification(callback);
    NSString* strObjName = charToString(objName);
    [callback setObjectName:strObjName];
    
    popupView = [[GFPopupView alloc] init:callback];
}

void setPopupAdSchedule_(int num) {
    [popupView setSchedule:num];
    
}

void setPopupAdAnimation_(bool flag) {
    BOOL objFlag = NO;
    if (flag) {
        objFlag = YES;
    }
    [popupView setAnimation:objFlag];
}

void popupLoadAd_(const char* siteId) {
    NSString* strSiteId = charToString(siteId);
    UIViewController* parent = getUnityViewController();
    if ([popupView loadAd:strSiteId]) {
        [parent.view addSubview:popupView];
        
        //        GFUnityDelegate* callback = [[GFUnityDelegate alloc] init];
        //        setApplicationNotification(callback);
    }
}

#pragma mark -
#pragma mark private
// privae
NSString* charToString(const char* c) {
    return [NSString stringWithCString:c encoding:NSUTF8StringEncoding];
}

// privae
UIViewController* getUnityViewController() {
    return UnityGetGLViewController();
}

// private
void setApplicationNotification(GFUnityDelegate* callback) {
    NSNotificationCenter *nc = [NSNotificationCenter defaultCenter];
    [nc addObserver:callback selector:@selector(applicationDidEnterBackground) name:UIApplicationDidEnterBackgroundNotification object:nil];
    [nc addObserver:callback selector:@selector(applicationWillEnterForeground) name:UIApplicationWillEnterForegroundNotification object:nil];
}
