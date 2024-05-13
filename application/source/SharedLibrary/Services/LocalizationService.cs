#nullable enable

using Microsoft.Extensions.Localization;
using SharedLibrary.Models.ActionCenter;
using SharedLibrary.Models.Form;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace SharedLibrary.Services
{
    public class LocalizationService : IStringLocalizer
    {

        // Por padrão o serviço pega os Resources da assembly compartilhada - SharedLibrary
        public LocalizationService(CultureInfo culture, string? alternateResourcesPath = null, Assembly? alternateAssembly = null)
        {
            CurrentCulture = culture;
            AlternateResourcesPath = alternateResourcesPath;
            AlternateAssembly = alternateAssembly;
        }

        public CultureInfo CurrentCulture { get; set; } = new CultureInfo("pt-BR");

        private string SharedLibraryResourcesPath = "SharedLibrary.Resources.Languages";
        private readonly string? AlternateResourcesPath;
        private readonly Assembly? AlternateAssembly;

        // Construtor parapegar os Resources da assembly compartilhada - SharedLibrary
        public ResourceManager ResourceManager()
        {
            return new ResourceManager(SharedLibraryResourcesPath + "." + CurrentCulture.ToString(), typeof(LoginFormModel).Assembly);
        }

        // Construtor parapegar os Resources da assembly sendo executada, por exemplo, cliente local Desktop
        public ResourceManager ResourceManagerExecutingAssembly()
        {
            return new ResourceManager(AlternateResourcesPath + "." + CurrentCulture.ToString(), AlternateAssembly);
        }

        public void SetLanguage(string language)
        {
            CurrentCulture = new CultureInfo(language);
        }

        /// <summary>
        /// Pega string da assembly compartilhada - SharedLibrary
        /// </summary>
        /// <param name="resourceName">Nome do Resource</param>
        /// <returns></returns>
        public LocalizedString this[string resourceName] {
            get {
                ResourceManager rm = ResourceManager();
                return new LocalizedString(resourceName, rm.GetString(resourceName, CurrentCulture));
            }
        }

        /// <summary>
        /// Pega string da assembly compartilhada - SharedLibrary - com resources extras
        /// </summary>
        /// <param name="resourceName">Nome do Resource</param>
        /// <param name="arguments">Resources extras para serem substituídos na string do resource principal</param>
        /// <returns></returns>
        public LocalizedString this[string resourceName, params object[] arguments] {
            get {
                ResourceManager rm = ResourceManager();
                return new LocalizedString(resourceName, String.Format(rm.GetString(resourceName, CurrentCulture), arguments));
            }
        }

        /// <summary>
        /// Recebe uma Tarefa do ActionCenter e retorna a string
        /// </summary>
        /// <param name="taskMessage">Mensagem da tarefa do ActionCenter</param>
        /// <returns></returns>
        public LocalizedString this[ActionCenterTaskMessageModel taskMessage] {
            get {
                ResourceManager rm = ResourceManager();

                if (taskMessage.AdditionalResourcesNames == null)
                {
                    return new LocalizedString(taskMessage.ResourceName, rm.GetString(taskMessage.ResourceName, CurrentCulture));
                } else
                {
                    return new LocalizedString(taskMessage.ResourceName, String.Format(rm.GetString(taskMessage.ResourceName, CurrentCulture), taskMessage.AdditionalResourcesNames.ToArray()));
                }
            }
        }

        /// <summary>
        /// Se <paramref name="getFromAlternateAssembly"/> for <c>true</c> pega a string da assembly sendo executada, por exemplo, cliente local Desktop
        /// </summary>
        /// <param name="resourceName">Nome do Resource</param>
        /// <param name="getFromAlternateAssembly">Pegar da assembly alternativa?</param>
        /// <returns></returns>
        public LocalizedString this[string name, bool getFromAlternateAssembly] {
            get {
                ResourceManager rm;
                if (getFromAlternateAssembly)
                {
                    rm = ResourceManagerExecutingAssembly();
                }
                else
                {
                    rm = ResourceManager();
                }
                return new LocalizedString(name,
                    rm.GetString(name, CurrentCulture));
            }
        }

        /// <summary>
        /// Se <paramref name="getFromAlternateAssembly"/> for <c>true</c> pega a string da assembly sendo executada, por exemplo, cliente local Desktop
        /// </summary>
        /// <param name="resourceName">Nome do Resource</param>
        /// <param name="getFromAlternateAssembly">Pegar da assembly alternativa?</param>
        /// <param name="arguments">Resources extras para serem substituídos na string do resource principal</param>
        /// <returns></returns>
        public LocalizedString this[string name, bool getFromAlternateAssembly, params object[] arguments] {
            get {
                ResourceManager rm;
                if (arguments[0] is bool)
                {
                    rm = ResourceManagerExecutingAssembly();
                }
                else
                {
                    rm = ResourceManager();
                }
                return new LocalizedString(name,
                    rm.GetString(name, CurrentCulture));
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            ResourceManager rm = ResourceManager();
            ResourceSet? resourceSet = rm.GetResourceSet(CurrentCulture, false, true);

            if (resourceSet != null)
            {
                IDictionaryEnumerator enumerator = resourceSet.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    if (enumerator.Entry.Value != null)
                    {
                        yield return new LocalizedString((string)enumerator.Entry.Key, (string)enumerator.Entry.Value);
                    } else
                    {
                        throw new System.ArgumentNullException($"Resource entry not found! Parameter {nameof(enumerator.Entry)} cannot be null", nameof(enumerator.Entry));
                    }
                }

                //foreach (DictionaryEntry? value in res)
                //{
                //    yield return new LocalizedString((string)value.Key, (string)value.Value);
                //}
            } else
            {
                throw new System.ArgumentNullException($"Resource set not found! Parameter {nameof(resourceSet)} cannot be null", nameof(resourceSet));
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new LocalizationService(culture, AlternateResourcesPath);
        }

        internal class GreetingResources
        {
        }
    }
}
