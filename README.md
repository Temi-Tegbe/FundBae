# FundBae

Users can create profiles and accounts. 
Users can fund and withdraw from their accounts. 
Users can have up to five accounts. 
When a user creates a profile, they're automatically assigned an account with a hard-coded type.

Endpoints

Customer/CreateCustomer

FirstName
LastName
EmailAddress
PhoneNumber
Password
DateRegistered
DateModified
Account with AccountNumber autogenerated and account type Flex is created

Customer/Login

EmailAddress
Password


Customers/GetAllCustomers

PageSize
PageNumber

Customers/GetAllCustomersWithZeroBalance

PageSize
PageNumber


Account/CreateAccount

Id
AccountName
AccountNumber is autogenerated when the service is called
AccountType 
CustomerId


Account/CreditAccount

AccountNumber
Amount 

Account/DebitAccount

AccountNumber
Amount



Account/GetAllAccounts

PageSize
PageNumber

Account/GetAllAccountsWithNoCustomer
PageSize
PageNumber


Interest/GetInterest
AccountNumber

