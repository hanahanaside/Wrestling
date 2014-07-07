//
//  APUnityDelegate.h
//
//  Created by KazukiOhashi on 13/10/23.
//  Copyright (c) 2013年 AMoAd inc. All rights reserved.
//


#import "AMoAdSDK.h"

#define UD_RES_STS				@"AppliPromotion::ResSts"
#define UD_RES_URL				@"AppliPromotion::ResURL"
#define UD_RES_WIDTH			@"AppliPromotion::ResWidth"
#define UD_RES_HEIGHT			@"AppliPromotion::ResHeight"

@interface APUnityDelegate : NSObject

@property (nonatomic, retain) NSString* objectName;

- (void)setCloseView:(UIViewController *)amoAdView parentView:(UIViewController *)parent;
- (void)applicationDidEnterBackground;

@end
