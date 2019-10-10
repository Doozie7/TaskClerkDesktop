#define CODE_ANALYSIS

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("TaskClerk.SDK")]
[assembly: AssemblyDescription("The TaskClerk software development kit.")]
[assembly: AssemblyCompany("BritishMicro")]
[assembly: AssemblyProduct("TaskClerk.SDK")]
[assembly: AssemblyCopyright("Copyright © BritishMicro 2015")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("dfaa9159-e350-4aa9-91ad-01b2ef3ed062")]

//The Common Language Specification (CLS) defines naming restrictions, 
//data types, and rules to which assemblies must conform if they are to be used 
//across programming languages. Good design dictates that all assemblies 
//explicitly indicate CLS compliance with CLSCompliantAttribute. 
//If the attribute is not present on an assembly, the assembly is not compliant.
[assembly: CLSCompliant(true)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.1.111")]
[assembly: AssemblyFileVersion("1.0.1.0")]

//Assemblies specify security permission requests to communicate to administrators
//the minimum permissions required to execute the assembly, and to limit security 
//vulnerabilities caused by mistakenly omitting demands at the type and member level.
//[assembly : SecurityPermission(SecurityAction.RequestMinimum, Execution = true)]
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "BritishMicro.TaskClerk.Plugins")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "BritishMicro.TaskClerk.Settings")]

[assembly: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "BritishMicro.TaskClerk.TaskDescription.Url")]
[assembly: NeutralResourcesLanguage("en")]
