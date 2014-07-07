//
//  APUnityDelegate.m
//
//  Created by KazukiOhashi on 13/10/23.
//  Copyright (c) 2013年 AMoAd inc. All rights reserved.
//

#import "APUnityDelegate.h"

#define UNITY_RESPONSE_PARSECHAR	@","

extern void UnitySendMessage(const char* obj, const char* method, const char* msg);

@implementation APUnityDelegate  {
    UIViewController *parent;
    UIViewController *amoAdView;
}

@synthesize objectName;

#pragma mark - callback
- (void)setCloseView:(UIViewController *)childView parentView:(UIViewController *)parentView {
    if (amoAdView != NULL) {
        [amoAdView release];
    }
    amoAdView = childView;
    parent = parentView ;
    
    [amoAdView retain];
}

- (void)applicationDidEnterBackground {
    NSNotificationCenter *nc = [NSNotificationCenter defaultCenter];
    [nc removeObserver:self name:UIApplicationDidEnterBackgroundNotification object:nil];
    if (amoAdView != NULL && parent != NULL) {
        [parent dismissModalViewControllerAnimated:NO];
        [amoAdView release];
        amoAdView = NULL;
    }
    
}


- (void) dealloc {
    if (amoAdView != NULL) {
        [amoAdView release];
        amoAdView = NULL;
    }
    [super dealloc];
}

// callback for unity
- (void) returnPopup {
	NSUserDefaults *ud = [NSUserDefaults standardUserDefaults];
	NSString *resStr = [ud objectForKey:UD_RES_STS];

    const char* resChr = [resStr UTF8String];
    const char* objName = [self.objectName UTF8String];
    UnitySendMessage(objName, "returnPopup", resChr);
}
@end
