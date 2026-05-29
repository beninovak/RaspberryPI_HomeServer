namespace HomeServer.Controllers;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class SkipAuthAttribute : Attribute {}