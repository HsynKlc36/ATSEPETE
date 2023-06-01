namespace AtSepete.Business.Constants;

public class Messages
{
    public const string LoginSuccess = "Login_Success";
    public const string LoginFailed = "Login_Failed";
    public const string LogOutSuccess = "LogOut_Success";
    public const string LogOutFailed = "LogOut_Failed";



    public const string ListedSuccess = "Listed_Success";
    public const string ListedFailed = "Listed_Failed";
    public const string ListReceived = "List_Received";
    public const string ListNotReceived = "List_Not_Received";
    public const string InvalidParameter = "Invalid_Parameter";

    public const string EmailDuplicate = "Email_Duplicate";
    public const string EmailFailed = "Email_Failed";
    public const string ResetPasswordEmailSender_Fail= "ResetPasswordEmailSender_Fail";
    public const string ResetPasswordEmailSender_Success= "ResetPasswordEmailSender_Success";
    public const string EmailOrPasswordInvalid = "Email_Or_Password_Invalid";
    public const string UserNotFound = "User_Not_Found";
    public const string UsersNotFound = "Users_Not_Found";
    public const string AddUserSuccess = "Add_User_Success";
    public const string UserFoundSuccess = "User_Found_Success";
    public const string UserFoundFail = "User_Found_Fail";
    public const string UsersFoundSuccess = "Users_Found_Success";
    public const string UserTokenFoundSuccess = "Users_Token_Found_Success";
    public const string UserTokenNotFound = "Users_Token_Not_Found";
    public const string UserTokenFoundFail = "Users_Token_Found_Fail";
    public const string FoundSuccess = "Found_Successfully";

    public const string AddSuccess = "Add_Success";
    public const string AddFail = "Add_Fail";
    public const string AddUserFail = "Add_User_Fail";
    public const string AddUserRoleFail = "Add_User_Role_Fail";
    public const string AddFailAlreadyExists = "Add_Fail_Already_Exists";

    public const string UpdateSuccess = "Update_Success";
    public const string UpdateFail = "Update_Fail";

    public const string DeleteSuccess = "Delete_Success";
    public const string DeleteFail = "Delete_Fail";

    public const string CategoryFoundSuccess = "Category_Found_Success";
    public const string CategoryNotFound = "Category_Not_Found";

    public const string ProductFoundSuccess = "Product_Found_Success";
    public const string ProductNotFound = "Product_Not_Found";

    public const string OrderFoundSuccess = "Order_Found_Success";
    public const string OrderNotFound = "Order_Not_Found";

    public const string OrderDetailFoundSuccess = "OrderDetail_Found_Success";
    public const string OrderDetailFailed = "OrderDetail_Failed";
    public const string OrderDetailNotFound = "OrderDetail_Not_Found";

    public const string MarketFoundSuccess = "Market_Found_Success";
    public const string MarketNotFound = "Market_Not_Found";

    public const string ProductMarketNotFound = "ProductMarket_Not_Found";
    public const string ProductMarketFoundSuccess = "ProductMarket_Found_Success";

    public const string ObjectNotValid = "Object_Not_Valid";
    public const string ObjectNotFound = "Object_Not_Found";
    public const string HardDeleteFail = "Hard_Delete_Fail";
    public const string HardDeleteSuccess = "Hard_Delete_Success";
    public const string SoftDeleteFail = "Soft_Delete_Fail";
    public const string SoftDeleteSuccess = "Soft_Delete_Success";


    public const string ChangePasswordSuccess = "Change_Password_Success";
    public const string ChangePasswordFail = "Change_Password_Fail";
    public const string ResetPasswordSuccess = "Reset_Password_Success";
    public const string ResetPasswordFail = "Reset_Password_Fail";
    public const string CheckPasswordNotValid = "Check_Password_Not_Valid";
    public const string CheckPasswordValid = "Check_Password_Valid";
    public const string CheckPasswordFail = "Check_Password_Fail";

    public const string PasswordNotMatch = "Password_Not_Match";
    public const string PasswordFail = "Password_Fail";
    public const string PasswordSuccess = "Password_Success";

    public const string ReportSuccess = "Report_Success";
    public const string ReportFailed = "Report_Failed";


 //Cons ve static atasındaki fark
//    const anahtar kelimesini kullanarak sabit bir değer tanımlamanız da mümkündür ve bu yaklaşımın birçok avantajı vardır.

//Örneğin, const anahtar kelimesiyle tanımlanan bir değişken, programın çalışması sırasında değeri değiştirilemez.Bu nedenle, const kullanarak tanımladığınız sabitler daha güvenlidir.

//Ancak, const anahtar kelimesi ile tanımlanan bir değişken, sadece derleme zamanında değeri belirlenebilir.Yani, const kullanarak tanımladığınız bir sabitin değerini çalışma zamanında değiştiremezsiniz.Bu sebeple, eğer loglama mesajlarınızı düzenli olarak güncellemek istiyorsanız static readonly kullanmanız daha uygun olabilir.

//Özetle, const anahtar kelimesi ile sabit bir değer tanımlamak hatalı bir kullanım olmaz ama loglama mesajları gibi dinamik olarak değişebilen veriler için static readonly kullanarak tanımlamanız daha mantıklı olabilir.


}