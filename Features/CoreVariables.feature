Feature: CoreVariables

A short summary of the feature

@Variables
Scenario:  Core variables scenario 1
	When Assign value "abc" to local variables "varname"
	Then Verify variable "varname" has value "abc"
