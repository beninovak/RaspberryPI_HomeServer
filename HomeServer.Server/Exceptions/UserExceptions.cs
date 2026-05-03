namespace HomeServer.Server.Exceptions;

public class FirstNameValidationException(string message) : Exception(message);
public class LastNameValidationException(string message) : Exception(message);
public class EmailValidationException(string message) : Exception(message);
public class PasswordValidationException(string message) : Exception(message);
public class UserInvalidLoginException(string message) : Exception(message);
public class NonExistentUserException(string message) : Exception(message);
