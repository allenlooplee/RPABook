# UiPath_TestingFramework #

Framework used for running of Unit and Functional tests in UiPath.

Quick guide:

* Install custom activity packages from **Gallery** tab of [Package manager](https://studio.uipath.com/docs/managing-activities-packages#section-managing-packages) or from the [UiPath Go](https://connect.uipath.com/profile/stefan-krsmanovic/components)
	* _Custom.Activities.VariableComparer.x.x.x.nupkg_
	* _TestingFrameworkUi.x.x.x.nupkg_
* Using **Unit Test** template (Tests_Repository\UnitTestTemplate\MethodName_StateUnderTest_ExpectedBehavior.xaml) make Unit Tests.
	* **Unit tests** must implement **Assert Unit Test** custom activity to be eligible for running in **Framework**.
* Place **Unit Tests** into the **Tests_Repository** folder. Feel free to make new subfolders inside of Tests_Repository folder, or its subfolders.
	* When **RunAllTests.xaml** is started it will open a GUI that will allow you to select which tests to run from **Tests_Repository** folder or its subfolders. GUI is also used to show test results.
* To integrate **TestingFramework** into another project: 
	* Copy whole folder to the target project root directory. 
	* Delete **project.json** file from **TestingFramework** folder (if present).
	* Add **Custom.Activities.VariableComparer.x.x.x.nupkg** and **TestingFrameworkUi.x.x.x.nupkg** packages as dependencies to the main projects **project.json** file. 

## Custom.Activities.VariableComparer.x.x.x.nupkg package ##

_Custom.Activities.VariableComparer.x.x.x.nupkg_ package is custom developed code for comparing variables, and is a core part of the Framework:

* It contains custom **Assert Unit Test** activity that must be used when making **Unit Tests**.
* **Assert Unit Test** activity is used as a container for writing **Assert methods** ([imported](https://studio.uipath.com/docs/importing-new-namespaces) from _CustomActivities.VariableComparer_ namespace) which are used to evaluate unit test outputs.
	* Assert.AreEqual(expected,actual) - contains method overloads that are used to check if the compared variables contain the exact same values.
	* Assert.AreNotEqual(expected,actual) - contains method overloads that are used to check if the compared variables doesn't contain the exact same values.
	* Assert.AreSame(expected,actual) - contains method overloads that are used to check if the compared variables reference the same object in memory.
	* Assert.AreNotSame(expected,actual) - contains method overloads that are used to check if the compared variables doesn't reference the same object in memory.
	* Assert.Contains(item, target) - contains method overloads that are used to check if the item is contained inside of the target object.
	* Assert.NotContains(expected,actual) - contains method overloads that are used to check if the item is not contained inside of the target object.
	* Assert.IsNull(actual) - checks if variable is referencing null object
	* Assert.IsNotNull(actual) - checks if variable is not referencing null object
* **Assert methods** throw custom exceptions that are used by the **Framework** to log test results and give important information in case of a test failiure.
	* AssertException is main exception thrown by Assert methods.
	* AssertNullException is exception thrown when Assert.IsNull or Assert.IsNotNull method returns false.
	* CustomAssertException is generic exception that is thrown when random boolean expression that returns false is passed to Assert Unit Test activity

## TestingFrameworkUi.x.x.x.nupkg package ##

_TestingFrameworkUi.x.x.x.nupkg_ package is a custom developed GUI made with WPF, and is a core part of the Framework:

GUI serves multiple purposes:

* Selecting which tests to run - GUI reads all the tests found in **Tests_Repository** folder and its subfolders.
* Showing the test results
* Allowing multiple test runs during single _RunAllTests.xaml_ execution

GUI quick guide:

* Select a specific test or a folder from the GUI tree view (use CTRL + left click to select multiple items).
* Run selected test(s) using one of the options:
	* Press Run All text at the top of the GUI.
	* Press Run Selected text at the top of the GUI (this can also be performed by right clicking on a tree view item and selecting appropriate option from the context menu).
	* Press Run Templates... text at the top of the GUI and choose appropriate option.
* After run is complete navigate the GUI to get the information about the ran tests.
* If needed perform additional test runs.
* When finished just close the GUI by pressing the "x" button.

NOTE: To edit test workflows while the Testing Framework is running, test needs to be opened in a second UiPath instance. 
This can quickly be done by right clicking the test and selecting **Edit test** option from the context menu.

## Framework ##

Whole idea behind the **Framework** was to make **UiPath Testing platform** that works as a _plug and play_ component that doesn't need to be configured before use.

Resulting product is a Framework that:

* Can be integrated into any UiPath project or framework (can be placed inside any folder or sub-folder and it will still work out of the box).
* Can run any _.xaml_ file found in **Test_Repository** folder.
* Logs test runs and gives extensive information about failed tests (both inside **UiPath** console and in textual log files).
	
## Unit tests ##

**Framework** has no use if there are no tests to be run.
As such, biggest effort required from the developers themselves is in writing of **Unit Tests**. 
Luckily, by using unit test template, custom activity and Assert methods, this is much less of a chore.

Basic idea of Unit Testing is to arrange a set of data that will be used to test the code.

After running of code that is being tested, output is compared with the expected value.  

Comparison should give information if test have passed or not.

Unit testing comprises of three stages: 

* **Arrange**
* **Act**
* **Assert**. 

It is recommended to make more than one test for one peace of code.  
This is to make sure that code correctly handles both good and bad data, and also to see if correct exceptions are produced by the code.   

**Tests_Repository\UnitTestExamples** folder contains few examples of how unit tests should look like.   
**Tests_Repository\UnitTestTemplate** folder contains master template for unit tests (_MethodName_StateUnderTest_ExpectedBehavior.xaml_).   
   
### Arrange Stage ###
   
Purpose of the arrange stage is to define input parameters for the test:

* Manually define parameters and data
* (If needed) Define **expected** result
* (If needed) Read configuration file

### Act Stage ###

Purpose of act stage is to run the code that is being tested (by invoking the code and supplying it previously arranged data). 

Output of this stage should be actual value that code produces assigned to the **actual** variable.

### Assert Stage ###

Purpose of the assert stage is to evaluate test outputs (usually this is done by comparing **actual** value gotten by running the code with the **expected value** that was defined in the arrange stage, but is not the only type of test that can be done).

For **Framework** to consider a **Unit Test** as valid, it must implement **Assert Unit Test activity**.
This activity is meant to be used along with, one of the many, **Assert methods** from _CustomActivities.VariableComparer_ namespace.

**Assert methods** make writing of Unit Tests much easier and exceptions thrown by these methods serve a big role in Framework logging.
