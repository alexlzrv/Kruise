namespace Kruise.Domain
{
    public static class Errors
    {
        public static class Post
        {
            public const string TitleCanNotBeNullOrWhiteSpace = "Title не может быть пустым или null";
            public const string TitleMaxLength = "Title не может быть больше {1} символов";
        }
    }
}
