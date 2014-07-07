//
//  NendPlugin.mm
//  Unity-iPhone
//
//  Created by ADN on 2013/11/06.
//
//

#import "NADView.h"
#import "NADIconLoader.h"
#import "NADIconView.h"
#import "iPhone_View.h"
#import "iPhone_target_Prefix.pch"

static NSMutableDictionary* _bannerAdDict = [[NSMutableDictionary alloc] init];
static NSMutableDictionary* _iconAdDict = [[NSMutableDictionary alloc] init];

enum NendGravity
{
    LEFT = 1,
    TOP = 2,
    RIGHT = 4,
    BOTTOM = 8,
    CENTER_VERTICAL = 16,
    CENTER_HORIZONTAL = 32,
};

enum NendOrientation
{
    HORIZONTAL = 0,
    VERTICAL = 1,
    UNSPECIFIED = 2,
};

enum NendBannerSize
{
    SIZE_320X50 = 0,
    SIZE_320X100 = 1,
    SIZE_300X100 = 2,
    SIZE_300X250 = 3,
    SIZE_728X90 = 4,
};

NSString* CreateNSString(const char* string)
{
    if ( string )
    {
        return @(string);
    }
    else
    {
        return @"";
    }
}

char* MakeStringCopy(const char* string)
{
    if ( NULL == string )
    {
        return NULL;
    }
    char* res = (char *)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

//==============================================================================

@interface NADViewEventDispatcher : NSObject <NADViewDelegate>

@property (nonatomic, copy) NSString* gameObject;

- (id) initWithGameObject:(NSString *)gameObject;

@end

@implementation NADViewEventDispatcher

- (id) initWithGameObject:(NSString *)gameObject
{
    self = [super init];
    if ( self )
    {
        _gameObject = [gameObject copy];
    }
    return self;
}

- (void) dealloc
{
    [_gameObject release];
    [super dealloc];
}

- (void) nadViewDidFinishLoad:(NADView *)adView
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdView_OnFinishLoad", "");
}

- (void) nadViewDidFailToReceiveAd:(NADView *)adView
{
    NSString* message = [NSString stringWithFormat:@"%d:%@", adView.error.code, adView.error.domain];
    UnitySendMessage([self.gameObject UTF8String], "NendAdView_OnFailToReceiveAd", MakeStringCopy([message UTF8String]));
}

- (void) nadViewDidReceiveAd:(NADView *)adView
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdView_OnReceiveAd", "");
}

- (void) nadViewDidClickAd:(NADView *)adView
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdView_OnClickAd", "");
}

@end

//==============================================================================

@interface NADIconLoaderEventDispatcher : NSObject <NADIconLoaderDelegate>

@property (nonatomic, copy) NSString* gameObject;

- (id) initWithGameObject:(NSString *)gameObject;

@end

@implementation NADIconLoaderEventDispatcher

- (id) initWithGameObject:(NSString *)gameObject
{
    self = [super init];
    if ( self )
    {
        _gameObject = [gameObject copy];
    }
    return self;
}

- (void) dealloc
{
    [_gameObject release];
    [super dealloc];
}

- (void) nadIconLoaderDidFinishLoad:(NADIconLoader *)iconLoader
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdIconLoader_OnFinishLoad", "");
}

- (void) nadIconLoaderDidFailToReceiveAd:(NADIconLoader *)iconLoader nadIconView:(NADIconView *)nadIconView
{
    NSString* message = [NSString stringWithFormat:@"%d:%@", iconLoader.error.code, iconLoader.error.domain];
    UnitySendMessage([self.gameObject UTF8String], "NendAdIconLoader_OnFailToReceiveAd", MakeStringCopy([message UTF8String]));
}

- (void) nadIconLoaderDidReceiveAd:(NADIconLoader *)iconLoader nadIconView:(NADIconView *)nadIconView
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdIconLoader_OnReceiveAd", "");
}

- (void) nadIconLoaderDidClickAd:(NADIconLoader *)iconLoader nadIconView:(NADIconView *)nadIconView
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdIconLoader_OnClickAd", "");
}

@end

//==============================================================================

@interface BannerParams : NSObject

@property (nonatomic, copy) NSString* gameObject;
@property (nonatomic, copy) NSString* apiKey;
@property (nonatomic, copy) NSString* spotID;
@property (nonatomic) BOOL outputLog;
@property (nonatomic) NSInteger size;
@property (nonatomic) NSInteger gravity;
@property (nonatomic) NSInteger leftMargin;
@property (nonatomic) NSInteger topMargin;
@property (nonatomic) NSInteger rightMargin;
@property (nonatomic) NSInteger bottomMargin;

+ (id) paramWithString:(NSString *)paramString;

@end

@implementation BannerParams

+ (id) paramWithString:(NSString *)paramString
{
    return [[[BannerParams alloc] initWithParamString:paramString] autorelease];
}

- (id) initWithParamString:(NSString *)paramString
{
    self = [super init];
    if ( self )
    {
        NSArray* paramArray = [paramString componentsSeparatedByString:@":"];
        int index = 0;
        _gameObject = [(NSString *)paramArray[index++] copy];
        _apiKey = [(NSString *)paramArray[index++] copy];
        _spotID = [(NSString *)paramArray[index++] copy];
        _outputLog = [@"true" isEqualToString:(NSString *)paramArray[index++]];
        _size = [paramArray[index++] integerValue];
        _gravity = [paramArray[index++] integerValue];
        _leftMargin = [paramArray[index++] integerValue];
        _topMargin = [paramArray[index++] integerValue];
        _rightMargin = [paramArray[index++] integerValue];
        _bottomMargin = [paramArray[index++] integerValue];
    }
    return self;
}

- (void) dealloc
{
    [_gameObject release];
    [_apiKey release];
    [_spotID release];
    [super dealloc];
}

@end

//==============================================================================

@interface Icon : NSObject

@property (nonatomic) NSInteger size;
@property (nonatomic) BOOL titleVisible;
@property (nonatomic) BOOL spaceEnabled;
@property (nonatomic, copy) NSString* titleColor;
@property (nonatomic) NSInteger gravity;
@property (nonatomic) NSInteger leftMargin;
@property (nonatomic) NSInteger topMargin;
@property (nonatomic) NSInteger rightMargin;
@property (nonatomic) NSInteger bottomMargin;

+ (id) iconWithString:(NSString*)paramString;

@end

@implementation Icon

+ (id) iconWithString:(NSString *)paramString
{
    return [[[Icon alloc] initWithParamString:paramString] autorelease];
}

- (id) initWithParamString:(NSString *)paramString
{
    self = [super init];
    if ( self )
    {
        NSArray* paramArray = [paramString componentsSeparatedByString:@","];
        int index = 0;
        _size = [paramArray[index++] intValue];
        _spaceEnabled = [@"true" isEqualToString:(NSString *)paramArray[index++]];
        _titleVisible = [@"true" isEqualToString:(NSString *)paramArray[index++]];
        _titleColor = [(NSString *)paramArray[index++] copy];;
        _gravity = [paramArray[index++] integerValue];
        _leftMargin = [paramArray[index++] integerValue];
        _topMargin = [paramArray[index++] integerValue];
        _rightMargin = [paramArray[index++] integerValue];
        _bottomMargin = [paramArray[index++] integerValue];
    }
    return self;
}

- (void) dealloc
{
    [_titleColor release];
    [super dealloc];
}

@end

//==============================================================================

@interface IconParams : NSObject

@property (nonatomic, copy) NSString* gameObject;
@property (nonatomic, copy) NSString* apiKey;
@property (nonatomic, copy) NSString* spotID;
@property (nonatomic) BOOL outputLog;
@property (nonatomic) NSInteger orientation;
@property (nonatomic) NSInteger gravity;
@property (nonatomic) NSInteger leftMargin;
@property (nonatomic) NSInteger topMargin;
@property (nonatomic) NSInteger rightMargin;
@property (nonatomic) NSInteger bottomMargin;
@property (nonatomic) NSInteger iconCount;
@property (nonatomic, retain) NSMutableArray* iconArray;

+ (id) paramWithString:(NSString *)paramString;

@end

@implementation IconParams

+ (id) paramWithString:(NSString *)paramString
{
    return [[[IconParams alloc] initWithParamString:paramString] autorelease];
}

- (id) initWithParamString:(NSString *)paramString
{
    self = [super init];
    if ( self )
    {
        NSArray* paramArray = [paramString componentsSeparatedByString:@":"];
        int index = 0;
        _gameObject = [(NSString *)paramArray[index++] copy];
        _apiKey = [(NSString *)paramArray[index++] copy];
        _spotID = [(NSString *)paramArray[index++] copy];
        _outputLog = [@"true" isEqualToString:(NSString *)paramArray[index++]];
        _orientation = [paramArray[index++] integerValue];
        _gravity = [paramArray[index++] integerValue];
        _leftMargin = [paramArray[index++] integerValue];
        _topMargin = [paramArray[index++] integerValue];
        _rightMargin = [paramArray[index++] integerValue];
        _bottomMargin = [paramArray[index++] integerValue];
        _iconCount = [paramArray[index++] integerValue];
        _iconArray = [[NSMutableArray alloc] init];
        for ( int i = 0; i < _iconCount; i++ )
        {
            [_iconArray addObject:[Icon iconWithString:paramArray[index + i]]];
        }
    }
    return self;
}

- (void) dealloc
{
    [_gameObject release];
    [_apiKey release];
    [_spotID release];
    [_iconArray release];
    [super dealloc];
}

@end

//==============================================================================

@interface IconHolder : NSObject

@property (nonatomic, retain) NADIconLoader* loader;
@property (nonatomic, retain) NSMutableArray* iconViewArray;

+ (id) holderWithIconLoader:(NADIconLoader *)loader iconViewArray:(NSMutableArray *)iconViewArray;

@end

@implementation IconHolder

+ (id) holderWithIconLoader:(NADIconLoader *)loader iconViewArray:(NSMutableArray *)iconViewArray
{
    return [[[IconHolder alloc] initWithIconLoader:loader iconViewArray:iconViewArray] autorelease];
}

- (id) initWithIconLoader:(NADIconLoader *)loader iconViewArray:(NSMutableArray *)iconViewArray
{
    self = [super init];
    if ( self )
    {
        _loader = [loader retain];
        _iconViewArray = [iconViewArray retain];
    }
    return self;
}

- (void) dealloc
{
    for ( NADIconView* iconView in _iconViewArray )
    {
        [iconView removeFromSuperview];
        [_loader removeIconView:iconView];
    }
    [_iconViewArray removeAllObjects];
    
    [_loader.delegate release];
    _loader.delegate = nil;
    
    [_loader release];
    [_iconViewArray release];
    
    _loader = nil;
    _iconViewArray = nil;
    
    [super dealloc];
}

@end

//==============================================================================

CGSize BannerSize(NendBannerSize size)
{
    switch ( size )
    {
        case SIZE_320X50:
            return CGSizeMake(320, 50);
        case SIZE_320X100:
            return CGSizeMake(320, 100);
        case SIZE_300X100:
            return CGSizeMake(300, 100);
        case SIZE_300X250:
            return CGSizeMake(300, 250);
        case SIZE_728X90:
            return CGSizeMake(728, 90);
        default:
            return CGSizeZero;
    }
}

CGPoint CalculatePointFromGravityAndMargins(int gravity, CGSize viewSize, int left, int top, int right, int bottom)
{
    CGPoint point = CGPointZero;
    CGSize screenSize = UnityGetGLView().bounds.size;
    
    if ( 0 != (gravity & CENTER_HORIZONTAL) )
    {
        point.x = (screenSize.width - viewSize.width) / 2;
    }
    if ( 0 != (gravity & RIGHT) )
    {
        point.x = screenSize.width - viewSize.width;
    }
    if ( 0 != (gravity & LEFT) )
    {
        point.x = 0.0f;
    }
    
    if ( 0 != (gravity & CENTER_VERTICAL) )
    {
        point.y = (screenSize.height - viewSize.height) / 2;
    }
    if ( 0 != (gravity & BOTTOM) )
    {
        point.y = screenSize.height - viewSize.height;
    }
    if ( 0 != (gravity & TOP) )
    {
        point.y = 0.0f;
    }
    
    point.x += left;
    point.y += top;
    point.x -= right;
    point.y -= bottom;

    return point;
}

UIColor* CreateUIColorFromColorCode(NSString* colorCode)
{
    if ( !colorCode || 0 == colorCode.length )
    {
        return [UIColor blackColor];
    }
    
    if ( [[colorCode substringWithRange:NSMakeRange(0, 1)] isEqualToString:@"#"] )
    {
        colorCode = [colorCode substringWithRange:NSMakeRange(1, colorCode.length - 1)];
    }
    
    NSString *hexCodeStr;
    const char *hexCode;
    char *endptr;
    CGFloat red, green, blue;
    
    for ( NSInteger i = 0; i < 3; i++ )
    {
        hexCodeStr = [NSString stringWithFormat:@"+0x%@", [colorCode substringWithRange:NSMakeRange(i * 2, 2)]];
        hexCode    = [hexCodeStr cStringUsingEncoding:NSASCIIStringEncoding];
        
        switch (i) {
        case 0:
            red = strtol(hexCode, &endptr, 16);
            break;
        case 1:
            green = strtol(hexCode, &endptr, 16);
            break;
        case 2:
            blue = strtol(hexCode, &endptr, 16);
        default:
            break;
        }
    }
    
    return [UIColor colorWithRed:red / 255.0 green:green / 255.0 blue:blue / 255.0 alpha:1.0];
}

CGFloat IconActualHeight(Icon* icon)
{
    if ( !icon.spaceEnabled && icon.titleVisible )
    {
        return icon.size + icon.size * 15 / NAD_ICON_SIZE_57x57.height;
    }
    else
    {
        return icon.size;
    }
}

NADIconView* HiddenIconView(Icon* icon, CGRect frame)
{
    NADIconView* iconView = [[[NADIconView alloc] initWithFrame:frame] autorelease];
    iconView.hidden = YES;
    iconView.textColor = CreateUIColorFromColorCode(icon.titleColor);
    iconView.textHidden = !icon.titleVisible;
    iconView.iconSpaceEnabled = icon.spaceEnabled;
    return iconView;
}

//==============================================================================
//
//  Native Interface

extern "C"
{
	void _TryCreateBanner(const char* paramString)
    {
        BOOL didLoaded = NO;
        BannerParams* params = [BannerParams paramWithString:CreateNSString(paramString)];
        NADView* adView = _bannerAdDict[params.gameObject];
        
        if ( !adView )
        {
            CGSize bannerSize = BannerSize((NendBannerSize)params.size);
            CGPoint point = CalculatePointFromGravityAndMargins(params.gravity,
                                                                bannerSize,
                                                                params.leftMargin,
                                                                params.topMargin,
                                                                params.rightMargin,
                                                                params.bottomMargin);
            
            adView = [[[NADView alloc] initWithFrame:CGRectMake(point.x, point.y, bannerSize.width, bannerSize.height)] autorelease];
            adView.hidden = YES;
            [adView setIsOutputLog:params.outputLog];
            [adView setNendID:params.apiKey spotID:params.spotID];
            adView.delegate = [[NADViewEventDispatcher alloc] initWithGameObject:params.gameObject];
            _bannerAdDict[params.gameObject] = adView;
        }
        else
        {
            didLoaded = YES;
        }
        
        if ( !adView.superview )
        {
            [UnityGetGLView() addSubview:adView];
        }
        
        if ( !didLoaded )
        {
            [adView load];
        }
    }
    
    void _ShowBanner(const char* gameObject)
    {
        NADView* adView = _bannerAdDict[CreateNSString(gameObject)];
        if ( adView )
        {
            adView.hidden = NO;
        }
    }
    
	void _HideBanner(const char* gameObject)
    {
        NADView* adView = _bannerAdDict[CreateNSString(gameObject)];
        if ( adView )
        {
            adView.hidden = YES;
        }
    }
    
	void _ResumeBanner(const char* gameObject)
    {
        NADView* adView = _bannerAdDict[CreateNSString(gameObject)];
        if ( adView )
        {
            [adView resume];
        }
    }
    
	void _PauseBanner(const char* gameObject)
    {
        NADView* adView = _bannerAdDict[CreateNSString(gameObject)];
        if ( adView )
        {
            [adView pause];
        }
    }
    
	void _DestroyBanner(const char* gameObject)
    {
        NADView* adView = _bannerAdDict[CreateNSString(gameObject)];
        if ( adView )
        {
            [adView removeFromSuperview];
            [adView.delegate release];
            adView.delegate = nil;
            [_bannerAdDict removeObjectForKey:CreateNSString(gameObject)];
        }
    }
    
    void _TryCreateIcons(const char* paramString)
    {
        BOOL didLoaded = NO;
        IconParams* params = [IconParams paramWithString:CreateNSString(paramString)];
        IconHolder* holder = _iconAdDict[params.gameObject];
        
        if ( !holder )
        {
            NADIconLoader* iconLoader = [[[NADIconLoader alloc] init] autorelease];
            [iconLoader setIsOutputLog:params.outputLog];
            NSMutableArray* iconArray = [NSMutableArray array];
            
            if ( UNSPECIFIED != params.orientation )
            {
                CGFloat width = 0.0f;
                CGFloat height = 0.0f;

                if ( VERTICAL == params.orientation )
                {
                    CGFloat iconWidth = 0.0f;
                    for ( Icon* icon in params.iconArray )
                    {
                        height += (IconActualHeight(icon) + icon.topMargin + icon.bottomMargin);
                        iconWidth = icon.size + icon.leftMargin + icon.rightMargin;
                        if ( width < iconWidth )
                        {
                            width = iconWidth;
                        }
                    }
                }
                else
                {
                    CGFloat iconHeight = 0.0f;
                    for ( Icon* icon in params.iconArray )
                    {
                        width += (icon.size + icon.leftMargin + icon.rightMargin);
                        iconHeight = IconActualHeight(icon) + icon.topMargin + icon.bottomMargin;
                        if ( height < iconHeight )
                        {
                            height = iconHeight;
                        }
                    }
                }
                
                CGPoint point = CalculatePointFromGravityAndMargins(params.gravity,
                                                                    CGSizeMake(width, height),
                                                                    params.leftMargin,
                                                                    params.topMargin,
                                                                    params.rightMargin,
                                                                    params.bottomMargin);
                
                for ( Icon* icon in params.iconArray )
                {
                    CGRect frame;
                    if ( VERTICAL == params.orientation )
                    {
                        frame = CGRectMake(point.x + icon.leftMargin - icon.rightMargin, point.y + icon.topMargin, icon.size, icon.size);
                        point.y += icon.topMargin;
                        point.y += icon.size;
                        point.y += icon.bottomMargin;
                    }
                    else
                    {
                        frame = CGRectMake(point.x + icon.leftMargin, point.y + icon.topMargin - icon.bottomMargin, icon.size, icon.size);
                        point.x += icon.leftMargin;
                        point.x += icon.size;
                        point.x += icon.rightMargin;
                    }
                    [iconArray addObject:HiddenIconView(icon, frame)];
                }
            }
            else
            {
                for ( Icon* icon in params.iconArray )
                {
                    CGPoint point = CalculatePointFromGravityAndMargins(icon.gravity,
                                                                        CGSizeMake(icon.size, IconActualHeight(icon)),
                                                                        icon.leftMargin,
                                                                        icon.topMargin,
                                                                        icon.rightMargin,
                                                                        icon.bottomMargin);
                    [iconArray addObject:HiddenIconView(icon, CGRectMake(point.x, point.y, icon.size, icon.size))];
                }
            }
            
            for ( NADIconView* iconView in iconArray )
            {
                [iconLoader addIconView:iconView];
            }
            [iconLoader setNendID:params.apiKey spotID:params.spotID];
            iconLoader.delegate = [[NADIconLoaderEventDispatcher alloc] initWithGameObject:params.gameObject];
            holder = [IconHolder holderWithIconLoader:iconLoader iconViewArray:iconArray];
            _iconAdDict[params.gameObject] = holder;
        }
        else
        {
            didLoaded = YES;
        }
        
        for ( NADIconView* iconView in holder.iconViewArray )
        {
            if ( !iconView.superview )
            {
                [UnityGetGLView() addSubview:iconView];
            }
        }
        
        if ( !didLoaded )
        {
            [holder.loader load];
        }
    }
    
    void _ShowIcons(const char* gameObject)
    {
        IconHolder* holder = _iconAdDict[CreateNSString(gameObject)];
        if ( holder )
        {
            for ( NADIconView* iconView in holder.iconViewArray )
            {
                iconView.hidden = NO;
            }
        }
    }
    
	void _HideIcons(const char* gameObject)
    {
        IconHolder* holder = _iconAdDict[CreateNSString(gameObject)];
        if ( holder )
        {
            for ( NADIconView* iconView in holder.iconViewArray )
            {
                iconView.hidden = YES;
            }
        }
    }
    
	void _ResumeIcons(const char* gameObject)
    {
        IconHolder* holder = _iconAdDict[CreateNSString(gameObject)];
        if ( holder )
        {
            [holder.loader resume];
        }
    }
    
	void _PauseIcons(const char* gameObject)
    {
        IconHolder* holder = _iconAdDict[CreateNSString(gameObject)];
        if ( holder )
        {
            [holder.loader pause];
        }
    }
    
	void _DestroyIcons(const char* gameObject)
    {
        [_iconAdDict removeObjectForKey:CreateNSString(gameObject)];
    }
}