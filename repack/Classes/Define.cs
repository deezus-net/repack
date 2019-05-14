namespace repack.Classes
{
    public class Define
    {
        public class AccountType
        {
            public const string User = "user";
            public const string Administrator = "admin";
        }

        public class TaskConditionType
        {
            public const string Equal = "eq";
            public const string NotEqual = "neq";
        }

        public class LogType
        {
            public const string LoginSuccess = "login_sucess";
            public const string LoginFailure = "login_failure";
            public const string WebHook = "webhook";
        }
    }
}