﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ValidationErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Application.Resources.ValidationErrorMessages", typeof(ValidationErrorMessages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Eposta adresi boş olamaz.
        /// </summary>
        public static string EmailEmpty {
            get {
                return ResourceManager.GetString("EmailEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Lütfen geçerli bir eposta adresi girin.
        /// </summary>
        public static string EmailNotValid {
            get {
                return ResourceManager.GetString("EmailNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Şifre en az bir sayı içermeli.
        /// </summary>
        public static string PasswordDigit {
            get {
                return ResourceManager.GetString("PasswordDigit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Şifre boş olamaz.
        /// </summary>
        public static string PasswordEmpty {
            get {
                return ResourceManager.GetString("PasswordEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Şifre uzunluğu yetersiz.
        /// </summary>
        public static string PasswordLength {
            get {
                return ResourceManager.GetString("PasswordLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Şifre en az bir küçük harf içermeli.
        /// </summary>
        public static string PasswordLowercaseLetter {
            get {
                return ResourceManager.GetString("PasswordLowercaseLetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Şifre en az bir özel karakter içermeli.
        /// </summary>
        public static string PasswordSpecialCharacter {
            get {
                return ResourceManager.GetString("PasswordSpecialCharacter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Şifre en az bir büyük harf içermeli.
        /// </summary>
        public static string PasswordUppercaseLetter {
            get {
                return ResourceManager.GetString("PasswordUppercaseLetter", resourceCulture);
            }
        }
    }
}
