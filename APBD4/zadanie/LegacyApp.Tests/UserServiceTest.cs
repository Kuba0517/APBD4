using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{
    

    [Fact]
    public void AddUser_Should_Return_False_When_Email_Is_Incorrect()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var addResult = userService.AddUser("Joe", "Doe", "johndoegmailcom",
            DateTime.Parse("1982-03-21"), 1);
        
        Assert.False(addResult);
    }
    
    

    [Fact]
    public void AddUser_Should_Return_False_When_Age_Lower_Than_21()
    {
        //Arrange
        var userService = new UserService();
        
        //Act
        var addResult = userService.AddUser("Joe", "Doe", "johndoe@gmail.com",
            DateTime.Parse("2015-03-21"), 1);
        
        //Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_User_Should_Throw_Exception_When_User_Doesnt_Exist()
    {
        //Arrange
        var userService = new UserService();
        
        //Act
        
        //Assert
        Assert.Throws<ArgumentException>(() =>
            userService.AddUser("Joeoij", "none", "johndoe@gmail.com",
                DateTime.Parse("1978-03-21"), 7)
        );
    }

    [Fact]
    public void AddUser_Should_Return_False_When_First_Name_Missing()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("", "Doe", "johndoe@gmail.com",
            DateTime.Parse("1970-03-21"), 1);
        //Assert
        Assert.False(addResult);

    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Last_Name_Missing()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("Joe", "", "johndoe@gmail.com",
            DateTime.Parse("1970-03-21"), 1);
        //Assert
        Assert.False(addResult);

    }


    [Fact]
    public void AddUser_Should_Return_False_When_Credit_Limit_Lower_Than_500()
    {
        
        //Arrange
        var userService = new UserService();
        
        //Act
        var addResult = userService.AddUser("Jan", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1970-03-21"), 1);
        
        //Assert
        Assert.False(addResult);

    }

    [Fact]
    public void AddUser_Should_Return_True_When_User_Is_Very_Important()
    {
        
        //Arrange
        var userService = new UserService();
        
        //Act
        var addResult = userService.AddUser("Jan", "Malewski", "malewski@gmail.pl", DateTime.Parse("1975-04-25"), 2);
        
        //Assert
        Assert.True(addResult);
    }


    [Fact]
    public void AddUser_Should_Return_True_When_User_Is_Important()
    {
        
        //Arrange
        var userService = new UserService();
        
        //Act
        var addResult = userService.AddUser("Jan", "Smith", "smith@gmail.pl", DateTime.Parse("1975-04-25"), 3);
        
        //Assert
        Assert.True(addResult);
        
    }
}