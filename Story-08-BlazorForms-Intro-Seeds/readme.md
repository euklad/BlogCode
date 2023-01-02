# BlazorForms low-code open-source framework introduction and seed projects

## Seed Projects

BlazorForms was in development for a few years as an internal project of PRO CODERS PTY LTD - Australian based consulting oriented on quality specialists, and now we decided to share it for the open-source community.

https://github.com/ProCodersPtyLtd/BlazorForms

https://procoders.com.au/

Our framework segregates an application to Flows, Forms and Rules. That prevents spaghetti coding and reduces amount of run-time errors, it also has full support of Visual Studio intellisence. 
To demonstrate what which component means and how it looks like it is better to open seed projects.

## BlazorFormsSeed

This project is created using Visual Studio Blazor Server App template, and then we added NuGet packages:

•	BlazorForms 0.7.0
•	BlazorForms.Rendering.MudBlazorUI 0.7.0

It also references indirectly to MudBlazor 6.1.5 – open-source framework implementing Material Design for Blazor.
![image](https://user-images.githubusercontent.com/6533278/210188312-29ec29f7-ad52-4480-ade3-abe4cbced6c3.png)

The navigation menu and layout were also changed to use MudBlazor.
When your run the application you can see the simple form generated dynamically, it is bound to a Model, supports validations and also it is a step in a Flow that controls which Form to show and which actions to do when Form is submitted or closed.
![image](https://user-images.githubusercontent.com/6533278/210188319-38eef3fc-c3f0-4821-8300-e42eaf0bf7ce.png)

All Sample code located in Flows\SampleFlow.cs file where we put a few classes together for simplicity.

## Summary
Here we presented BlazorForms framework – open-source project that simplifies Blazor UI development and allows to create simple and maintainable C# code, that is very useful for low-budget projects and prototyping. The main idea is to place logic to Flows and Rules which is not UI-depended and is unit-testable; and Forms simply contain bindings between Model and UI controls.
This is a brief presentation of BlazorForms and I have not covered different types of Flows, how to store Flow State between sessions, how to define Flows and Forms in Json, instead of C#, and many other features. All that will be presented in my Blog https://www.codeproject.com/script/Articles/MemberArticles.aspx?amid=5359093&pageflow=FixedWidth.

