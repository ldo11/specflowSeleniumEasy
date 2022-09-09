Feature: CoreSQLServer

A short summary of the feature

@SQLServer
Scenario:  Core sql server scenario 1
	When Set up connection string "connectionstring"
	And Execute SQL query "query" and save result
	And Save result at row "0" and column "0" to local variable "varname"
	Then Verify result at row "0" and column "0" contains "abc"
	Then Verify result contains list below
	| columnName | Value |
	| firstname  | Luat  |
	| lastname   | Do    |
	



