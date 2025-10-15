namespace Observer
{
    public static class UserFactory
    {
        public static Utente CreateUser(string username)
        {
            return new Utente(username);
        }
    }
}