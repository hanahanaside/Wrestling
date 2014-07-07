//
//  PyxisTrackingMain.h
//  PyxiskTracking
//
//  Created by USER on 11/04/13.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SystemConfiguration/SystemConfiguration.h>
#import <UIKit/UIKit.h>
#import <Foundation/NSString.h>

typedef enum appLimitMode{
	NONE = 0,
	DAILY = 1,
	DAU = 2
} AppLimitMode;
		
@interface PyxisTracking : NSObject {
}

// Global Class Getter
+ (NSString *) getPyxisFailParam;
+ (NSString *) getPyxisPId;
+ (NSNumber *) getPyxisSaveMax;
+ (NSNumber *) getPyxisSaveMode;
+ (NSString *) getPyxisSesId;
+ (BOOL) isPyxisLogMode;
+ (BOOL) isPyxisInitExeced;
+ (BOOL) isPyxisEnableCpi;
+ (BOOL) isPyxisFirst;
+ (BOOL) isPyxisCpiEnd;


// Global Class Setter
+ (void) setPyxisRmvTarget:(NSString *)rmvTarget;
+ (void) setPyxisFailParam:(NSString *)failParam;
+ (void) setPyxisPId:(NSString *)pId;
+ (void) setPyxisSaveMax:(NSNumber *)saveMax;
+ (void) setPyxisSaveMode:(NSNumber *)saveMode;
+ (void) setPyxisSesId:(NSString *)sesId;
+ (void) setPyxisLogMode:(BOOL)logMode;
+ (void) setPyxisInitExeced:(BOOL)initExeced;
+ (void) setPyxisEnableCpi:(BOOL)enableCpi;
+ (void) setPyxisFirst:(BOOL)first;

// Global Method
+ (void) initTrack;

+ (void) trackInstall:(NSString *)mv
                 suid:(NSString *)suid
                sales:(NSString *)sales
               volume:(NSString *)volume
               profit:(NSString *)profit
			   others:(NSString *)others
        launchOptions:(NSDictionary *)launchOptions;

+ (void) fromBrowser:(NSURL *)url;

+ (void) saveTrackApp:(NSString *)act_mv
			 act_suid:(NSString *)act_suid
            act_sales:(NSString *)act_sales
           act_volume:(NSString *)act_volume
            act_profit:(NSString *)act_profit
		   act_others:(NSString *)act_others;

+ (void) saveTrackApp:(NSString *)act_mv
			 act_suid:(NSString *)act_suid
            act_sales:(NSString *)act_sales
           act_volume:(NSString *)act_volume
		   act_profit:(NSString *)act_profit
		   act_others:(NSString *)act_others
	   app_limit_mode:(AppLimitMode *)app_limit_mode;

+ (void) sendTrackApp;

+ (void) saveAndSendTrackApp:(NSString *)act_mv
					act_suid:(NSString *)act_suid
                   act_sales:(NSString *)act_sales
                  act_volume:(NSString *)act_volume
                  act_profit:(NSString *)act_profit
				  act_others:(NSString *)act_others;

+ (void) saveAndSendTrackApp:(NSString *)act_mv
					act_suid:(NSString *)act_suid
                   act_sales:(NSString *)act_sales
                  act_volume:(NSString *)act_volume
                  act_profit:(NSString *)act_profit
				  act_others:(NSString *)act_others
			  app_limit_mode:(AppLimitMode *)app_limit_mode;

+ (void) setOptOut:(BOOL)isOptOut;
+ (BOOL) getOptOut;



+ (void) setValueToSum:(NSNumber *) _valueToSum;
+ (void) setFbLevel:(NSNumber *) fb_level;
+ (void) setFbSuccess:(NSNumber *) fb_success;
+ (void) setFbContentType:(NSString *) fb_content_type;
+ (void) setFbContentId:(NSString *) fb_content_id;
+ (void) setFbRegistrationMethod:(NSString *) fb_registration_method;
+ (void) setFbPaymentInfoAvailable:(NSNumber *) fb_payment_info_available;
+ (void) setFbMaxRatingValue:(NSNumber *) fb_max_rating_value;
+ (void) setFbNumItems:(NSNumber *) fb_num_items;
+ (void) setFbSearchString:(NSString *) fb_search_string;
+ (void) setFbDescription:(NSString *) fb_description;


+ (id) getInstance;

@end

