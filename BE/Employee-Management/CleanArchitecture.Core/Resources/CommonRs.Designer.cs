﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This Code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the Code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CleanArchitecture.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CommonRs {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CommonRs() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CleanArchitecture.Core.Resources.CommonRs", typeof(CommonRs).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Danh Sách Nhân Viên.
        /// </summary>
        internal static string EmployeeFileHeader {
            get {
                return ResourceManager.GetString("EmployeeFileHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Danh_Sach_Nhan_Vien.xlsx.
        /// </summary>
        internal static string EmployeeFileName {
            get {
                return ResourceManager.GetString("EmployeeFileName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Danh_Sach_Nhan_Vien.
        /// </summary>
        internal static string EmployeeWorkSheetName {
            get {
                return ResourceManager.GetString("EmployeeWorkSheetName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UploadedFiles.
        /// </summary>
        internal static string UploadFolderName {
            get {
                return ResourceManager.GetString("UploadFolderName", resourceCulture);
            }
        }
    }
}