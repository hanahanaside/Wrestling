#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "PyxisTracking.h"


////////////////////////////////////////////////////////////////////////////////
// Pyxis Unity Plugin Objecttive-C Native Part
// PyxisPlugin.m
// UnityからiOSネイティブコードへのリンケージがC言語のみであるため、ここでC→Objective-Cのスタブ処理を行っています。
// @auther		株式会社セプテーニ
// @version		1.0.0
////////////////////////////////////////////////////////////////////////////////

/**
 *  Log / NSDebugLog
 *  XCodeデバッグ環境でログを表示するマクロです。
 */
#ifdef DEBUG
#define Log(...) NSLog(__VA_ARGS__)
#define NSDebugLog(format, ...) NSLog((@"[Pyxis:Obj-C] %s [Line No. %d] " format), __PRETTY_FUNCTION__, __LINE__, ##__VA_ARGS__);
#else   // DEBUG
#define Log(...)
#define NSDebugLog(...)
#endif  // DEBUG

/**
 *  initTrack
 *  Pyxisライブラリへ接続するだけの関数です。
 */
void InitTrack() {
	// ライブラリをコールします。
	[PyxisTracking initTrack];
	NSDebugLog(@"PyxisTrackingLibrary initTrack called");
}

/**
 *  TrackInstall
 *  Pyxisライブラリへ接続するだけの関数です。
 */
void TrackInstall(
            const char* mv,
            const char* suid,
            const char* sales,
            const char* volume,
            const char* profit,
            const char* others) {
    // mvはC#側でチェック済みなので文字に変換します。
	NSString* mvStr = [NSString stringWithFormat:@"%s", mv];
	
	NSString* suidStr = nil;
	if (suid != NULL) {
		suidStr = [NSString stringWithCString:suid encoding:NSUTF8StringEncoding];
	}
	// salesはヌルを許可します。
	NSString* salesStr = nil;
	if (sales != NULL) {
		salesStr = [NSString stringWithFormat:@"%s", sales];
	}
	// volumeはヌルを許可します。
	NSString* volumeStr = nil;
	if (volume != NULL) {
		volumeStr = [NSString stringWithFormat:@"%s", volume];
	}
	// profitはヌルを許可します。
	NSString* profitStr = nil;
	if (profit != NULL) {
		profitStr = [NSString stringWithFormat:@"%s", profit];
	}
	// othersはヌルを許可します。
	NSString* othersStr = nil;
	if (others != NULL) {
		othersStr = [NSString stringWithCString:others encoding:NSUTF8StringEncoding];
	}
	NSDebugLog(@"TrackInstall params:");
	NSDebugLog(@"mv=%@", mvStr);
	NSDebugLog(@"suid=%@", suidStr);
	NSDebugLog(@"sales=%@", salesStr);
	NSDebugLog(@"volume=%@", volumeStr);
	NSDebugLog(@"profit=%@", profitStr);
	NSDebugLog(@"others=%@", othersStr);

	// ライブラリをコールします。
	[PyxisTracking trackInstall:mvStr
                          suid:suidStr
                         sales:salesStr
                        volume:volumeStr
                         profit:profitStr
                         others:othersStr
                 launchOptions:nil];
    NSDebugLog(@"PyxisTrackingLibrary trackInstall called");
}

/**
 *  SaveTrackApp
 *  Pyxisライブラリへ接続するだけの関数です。
 */
void SaveTrackApp(
            const char* mv,
            const char* suid,
            const char* sales,
            const char* volume,
            const char* profit,
            const char* others,
            const char* app_limit_mode) {
    // mvはC#側でチェック済みなので文字に変換します。
	NSString* mvStr = [NSString stringWithFormat:@"%s", mv];
	NSString* suidStr = nil;
	if (suid != NULL) {
		suidStr = [NSString stringWithCString:suid encoding:NSUTF8StringEncoding];
	}
	// salesはヌルを許可します。
	NSString* salesStr = nil;
	if (sales != NULL) {
		salesStr = [NSString stringWithFormat:@"%s", sales];
	}
	// volumeはヌルを許可します。
	NSString* volumeStr = nil;
	if (volume != NULL) {
		volumeStr = [NSString stringWithFormat:@"%s", volume];
	}
	// profitはヌルを許可します。
	NSString* profitStr = nil;
	if (profit != NULL) {
		profitStr = [NSString stringWithFormat:@"%s", profit];
	}
	// othersはヌルを許可します。
	NSString* othersStr = nil;
	if (others != NULL) {
		othersStr = [NSString stringWithCString:others encoding:NSUTF8StringEncoding];
	}
	// app_limit_modeはヌルを許可します。
	NSString* app_limit_modeStr = nil;
	if (app_limit_mode != NULL) {
		app_limit_modeStr = [NSString stringWithFormat:@"%s", app_limit_mode];
	}
	AppLimitMode* app_limit_modeALM = nil;
	if ([app_limit_modeStr isEqualToString:@"DAILY"]) {
		app_limit_modeALM = DAILY;
	} else if ([app_limit_modeStr isEqualToString:@"DAU"]) {
		app_limit_modeALM = DAU;
	} else {
		app_limit_modeALM = NONE;
	}

	NSDebugLog(@"SaveTrackApp params:");
	NSDebugLog(@"mvStr=%@", mvStr);
	NSDebugLog(@"suidStr=%@", suidStr);
	NSDebugLog(@"sales=%@", sales);
	NSDebugLog(@"volume=%@", volume);
	NSDebugLog(@"profit=%@", profit);
	NSDebugLog(@"others=%@", others);
	NSDebugLog(@"app_limit_mode=%@", app_limit_mode);
	

	// ライブラリをコールします。
	[PyxisTracking saveTrackApp:mvStr
                      act_suid:suidStr
                     act_sales:salesStr
                    act_volume:volumeStr
                     act_profit:profitStr
					act_others:othersStr
			app_limit_mode:app_limit_modeALM];
    NSDebugLog(@"PyxisTrackingLibrary saveTrackApp called");
}

/**
 *  SendTrackApp
 *  Pyxisライブラリへ接続するだけの関数です。
 */
void SendTrackApp() {
	// ライブラリをコールします。
	[PyxisTracking sendTrackApp];
    NSDebugLog(@"PyxisTrackingLibrary sendTrackApp called");
}

/**
 *  SaveAndSendTrackApp
 *  Pyxisライブラリへ接続するだけの関数です。
 */
void SaveAndSendTrackApp(
						 const char* mv,
						 const char* suid,
						 const char* sales,
						 const char* volume,
						 const char* profit,
						 const char* others,
						 const char* app_limit_mode) {
	
	// mvはC#側でチェック済みなので文字に変換します。
	NSString* mvStr = [NSString stringWithFormat:@"%s", mv];
	NSString* suidStr = nil;
	if (suid != NULL) {
		suidStr = [NSString stringWithCString:suid encoding:NSUTF8StringEncoding];
	}
	// salesはヌルを許可します。
	NSString* salesStr = nil;
	if (sales != NULL) {
		salesStr = [NSString stringWithFormat:@"%s", sales];
	}
	// volumeはヌルを許可します。
	NSString* volumeStr = nil;
	if (volume != NULL) {
		volumeStr = [NSString stringWithFormat:@"%s", volume];
	}
	// profitはヌルを許可します。
	NSString* profitStr = nil;
	if (profit != NULL) {
		profitStr = [NSString stringWithFormat:@"%s", profit];
	}
	// othersはヌルを許可します。
	NSString* othersStr = nil;
	if (others != NULL) {
		othersStr = [NSString stringWithCString:others encoding:NSUTF8StringEncoding];
	}
	// app_limit_modeはヌルを許可します。
	NSString* app_limit_modeStr = nil;
	if (app_limit_mode != NULL) {
		app_limit_modeStr = [NSString stringWithFormat:@"%s", app_limit_mode];
	}
	AppLimitMode* app_limit_modeALM = nil;
	if ([app_limit_modeStr isEqualToString:@"DAILY"]) {
		app_limit_modeALM = DAILY;
	} else if ([app_limit_modeStr isEqualToString:@"DAU"]) {
		app_limit_modeALM = DAU;
	} else {
		app_limit_modeALM = NONE;
	}
	
	NSDebugLog(@"SaveAndSendTrackApp params:");
	NSDebugLog(@"mvStr=%@", mvStr);
	NSDebugLog(@"suidStr=%@", suidStr);
	NSDebugLog(@"sales=%@", salesStr);
	NSDebugLog(@"volume=%@", volumeStr);
	NSDebugLog(@"profit=%@", profitStr);
	NSDebugLog(@"others=%@", othersStr);
	NSDebugLog(@"app_limit_mode=%@", app_limit_modeALM);
	
	// ライブラリをコールします。
	[PyxisTracking saveAndSendTrackApp:mvStr
                      act_suid:suidStr
                     act_sales:salesStr
                    act_volume:volumeStr
                     act_profit:profitStr
                     act_others:othersStr
				app_limit_mode:app_limit_modeALM];
    NSDebugLog(@"PyxisTrackingLibrary saveAndSendTrackApp called");
}

/**
 *  SetOptOut
 *  Pyxisライブラリへ接続するだけの関数です。
 */
void SetOptOut(const char* flg) {
	
	NSString* mvFlg = [NSString stringWithFormat:@"%s", flg];
	
	// ライブラリをコールします。
	[PyxisTracking setOptOut:mvFlg];
    NSDebugLog(@"PyxisTrackingLibrary setOptOut called");
}

/**
 *  SetOptOut
 */
bool GetOptOut() {
	
	// ライブラリをコールします。
	return [PyxisTracking getOptOut];
    NSDebugLog(@"PyxisTrackingLibrary getOptOut called");
}

/**
 *  SetValueToSum
 */
void SetValueToSum(const char* _valueToSum){
		
	NSString *_valueToSum_str = [NSString stringWithFormat:@"%s", _valueToSum];
	float _valueToSum_float = [_valueToSum_str floatValue];
	NSNumber *_valueToSum_ = [NSNumber numberWithFloat:_valueToSum_float];
	
	// ライブラリをコールします。
	return [PyxisTracking setValueToSum:_valueToSum_];
    NSDebugLog(@"PyxisTrackingLibrary setValueToSum called");
}

/**
 *  SetFbLevel
 */
void SetFbLevel(const char* fb_level){
		
	NSString *fb_level_str = [NSString stringWithFormat:@"%s", fb_level];
	int fb_level_int = [fb_level_str intValue];
	NSNumber *fb_level_ = [NSNumber numberWithInt:fb_level_int];
	
	// ライブラリをコールします。
	return [PyxisTracking setFbLevel:fb_level_];
    NSDebugLog(@"PyxisTrackingLibrary setFbLevel called");
}

/**
 *  SetFbSuccess
 */
void SetFbSuccess(const char* fb_success){
	
	NSString* fb_successStr = [NSString stringWithFormat:@"%s", fb_success];
	bool fb_success_bool = [fb_successStr boolValue];
	NSNumber *fb_success_ = [NSNumber numberWithBool:fb_success_bool];
	
	// ライブラリをコールします。
	return [PyxisTracking setFbSuccess:fb_success_];
    NSDebugLog(@"PyxisTrackingLibrary setFbSuccess called");
	
}

/**
 *  SetFbContentType
 */
void SetFbContentType(const char* fb_content_type){
	
	NSString *fb_content_type_ = [NSString stringWithCString:fb_content_type encoding:NSUTF8StringEncoding];
	
	// ライブラリをコールします。
	return [PyxisTracking setFbContentType:fb_content_type_];
    NSDebugLog(@"PyxisTrackingLibrary setFbContentType called");
	
}

/**
 *  SetFbContentId
 */
void SetFbContentId(const char* fb_content_id){
	
	NSString *fb_content_id_ = [NSString stringWithCString:fb_content_id encoding:NSUTF8StringEncoding];
	
	// ライブラリをコールします。
	return [PyxisTracking setFbContentId:fb_content_id_];
    NSDebugLog(@"PyxisTrackingLibrary setFbContentId called");
	
}

/**
 *  SetFbRegistrationMethod
 */
void SetFbRegistrationMethod(const char* fb_registration_method){
	
	NSString *fb_registration_method_ = [NSString stringWithCString:fb_registration_method encoding:NSUTF8StringEncoding];
	
	// ライブラリをコールします。
	return [PyxisTracking setFbRegistrationMethod:fb_registration_method_];
    NSDebugLog(@"PyxisTrackingLibrary setFbRegistrationMethod called");
	
}

/**
 *  SetFbPaymentInfoAvailable
 */
void SetFbPaymentInfoAvailable(const char* fb_payment_info_available){
		
	NSString* fb_payment_info_availableStr = [NSString stringWithFormat:@"%s", fb_payment_info_available];
	bool fb_payment_info_available_bool = [fb_payment_info_availableStr boolValue];
	NSNumber * fb_payment_info_available_ = [NSNumber numberWithBool:fb_payment_info_available_bool];
	
	// ライブラリをコールします。
	return [PyxisTracking setFbPaymentInfoAvailable:fb_payment_info_available_];
    NSDebugLog(@"PyxisTrackingLibrary setFbPaymentInfoAvailable called");
	
}

/**
 *  SetFbMaxRatingValue
 */
void SetFbMaxRatingValue(const char* fb_max_rating_value){
	
	NSString *fb_max_rating_value_str = [NSString stringWithFormat:@"%s", fb_max_rating_value];
	int fb_max_rating_value_int = [fb_max_rating_value_str intValue];
	NSNumber *fb_max_rating_value_ = [NSNumber numberWithInt:fb_max_rating_value_int];
	
	// ライブラリをコールします。
	return [PyxisTracking setFbMaxRatingValue:fb_max_rating_value_];
    NSDebugLog(@"PyxisTrackingLibrary setFbMaxRatingValue called");
	
}

/**
 *  SetFbNumItems
 */
void SetFbNumItems(const char* fb_num_items){
	
	NSString *fb_num_items_str = [NSString stringWithFormat:@"%s", fb_num_items];
	int fb_num_items_int = [fb_num_items_str intValue];
	NSNumber *fb_num_items_ = [NSNumber numberWithInt:fb_num_items_int];
	
	// ライブラリをコールします。
	return [PyxisTracking setFbNumItems:fb_num_items_];
    NSDebugLog(@"PyxisTrackingLibrary setFbNumItems called");
	
}

/**
 *  SetFbSearchString
 */
void SetFbSearchString(const char* fb_search_string){
	
	NSString *fb_search_string_ = [NSString stringWithCString:fb_search_string encoding:NSUTF8StringEncoding];
	
	// ライブラリをコールします。
	return [PyxisTracking setFbSearchString:fb_search_string_];
    NSDebugLog(@"PyxisTrackingLibrary setFbSearchString called");
	
}

/**
 *  SetFbDescription
 */
void SetFbDescription(const char* fb_description){
	
	NSString *fb_description_ = [NSString stringWithCString:fb_description encoding:NSUTF8StringEncoding];
	
	// ライブラリをコールします。
	return [PyxisTracking setFbDescription:fb_description_];
    NSDebugLog(@"PyxisTrackingLibrary setFbDescription called");
	
}
