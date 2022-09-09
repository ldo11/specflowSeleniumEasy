Feature: CoreVariables

A short summary of the feature

@Variables
Scenario:  Core variables scenario 1
	When Assign value "abc" to local variables "varname"
	Then Verify local variable "varname" has value "abc"

@Variables
Scenario:  Core variables scenario 2 compare
	When Assign value "10" to local variables "firstvar"
	And Assign value "10" to local variables "secondvar"
	And Assign value "100" to local variables "thirdvar"
	Then Verify local variable "firstvar" = "10"
	Then Verify local variable "firstvar" = "secondvar"
	Then Verify local variable "firstvar" < "15"
	Then Verify local variable "firstvar" < "thirdvar"
	Then Verify local variable "thirdvar" > "firstvar"


@Variables
Scenario:  Core variables scenario 3 filter and contains
	When Assign value "I have 10 years experiences" to local variables "authorintro"
	And Filter local variable "authorintro" using regex "\d+" and save to variable "years"
	And Filter local variable "authorintro" using regex "\d+"
	Then Verify local variable "authorintro" = "10"
	Then Verify local variable "authorintro" = "years"

