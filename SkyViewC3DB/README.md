# SkyViewC3 DB Demo Project

# Migration Setting

- Add Package

```
$ dotent add package Microsoft.EntityFrameworkCore.Sqlite

$ dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet
```

- Edit .csproj for using **EF** Command

```
// add this setting in .csproj
<DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.3" />
```

- Command Migration

```
$ dotnet ef migrations add [Migration Name]
$ dotnet ef database update
```

## Task about user

- Login
- Find User
- Update User Information
- Add User
- Find User with Conditional Permission
- Update User Grade
- Remove User

## Task about permission

- Add User Permission
- Update User Permission
- Remove User Permission

## Task about Log

- Add Log of data
- Add Log of device
- Add Log of user
- Get Log of data with conditional date
- Get Log of device with conditional date
- Get Log of user with conditional date

## Task about IMS

- Get All Rack Information
- Get Boxes in specific rack
- Get vial in specific box
- Add Box
- Remove Box
- Update Box for editting output column
- Update Box for editting input column
- Remove all vials in specific box
- Add Vial
- Remove Vial
- Find Box in specific Rack
- Find Vial in specific Box

- Get All Racks
- Get All Boxes
- Get All Vials
- IsExist Box
- IsExist Vial

## Tasl about IMS statistics

- Get All Statistic Info about IMS
