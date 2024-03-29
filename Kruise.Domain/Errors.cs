﻿namespace Kruise.Domain
{
    public static class Errors
    {
        public static class Post
        {
            public const string TitleCanNotBeNullOrWhiteSpace = "Title не может быть пустым или null";
            public const string TitleMaxLength = "Title не может быть больше {0} символов";
        }

        public static class Account
        {
            public const string NameCanNotBeNullOrWhiteSpace = "Name не может быть пустым или null";
            public const string NameMaxLength = "Name не может быть больше {0} символов";
        }
    }
}
