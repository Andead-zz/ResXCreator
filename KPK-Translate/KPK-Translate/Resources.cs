using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace KPK_Translate
{
    /// <summary>
    ///     Ресурсы
    /// </summary>
    /// <example>
    ///     1 Способ - создание файла локализации (Resources.en.resx) вручную
    ///     var resourceList = Resources.ExtractTranslatableResources(Path.Combine("C:\\", "Resources.resx"));
    ///     Resources.TranslateResources(list, Path.Combine("C:\\", "Resources.en.resx"));
    ///     2 Способ - автоматическое создание локализации с указанной культурой
    ///     var resourceList = Resources.ExtractTranslatableResources(Path.Combine("C:\\", "Resources.resx"));
    ///     Resources.TranslateResources(resourceList, Path.Combine("C:\\", "Resources.resx"), new CultureInfo("en-US"));
    /// </example>
    public static class Resources
    {
        /// <summary>
        ///     Получить список всех ресурсов доступных для перевода
        /// </summary>
        /// <param name="pathToResxFile">Путь к файлу ресурсов</param>
        /// <returns> Список ресурсов для перевода </returns>
        public static List<StringResource> ExtractTranslatableResources(string pathToResxFile)
        {
            // Находим файл
            var file = new FileInfo(pathToResxFile);
            // Если файл не найдет, то вернем null
            if (!file.Exists) return null;
            //  Загружаем файл
            XmlDocument xmlDocument;
            using (var xmlReader = XmlReader.Create(file.FullName))
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlReader);
            }
            // Получим все элементы с тегом "Data" - ресурсы
            var dataElems = xmlDocument.GetElementsByTagName("data");
            var localizable = new List<StringResource>();

            foreach (XmlNode one in dataElems)
            {
                // Если строка действительно локализуемый и это строка
                if (one.Attributes != null)
                {
                    var type = one.Attributes["type"];
                    var name = one.Attributes["name"];
                    var mimetype = one.Attributes["mimetype"];
                    // Если тип не строка(по умолчанию)
                    if (name == null || type != null || mimetype != null) continue;
                    if (name.Value.IndexOf(">>", StringComparison.Ordinal) != -1) continue;
                    var value = "";
                    var childNode = one.ChildNodes.Cast<XmlNode>().FirstOrDefault(node => node.Name == "value");
                    if (childNode == null) continue;
                    value = childNode.InnerText;

                    var comment = one.ChildNodes.Cast<XmlNode>().FirstOrDefault(node => node.Name == "comment");

                    var newRes = new StringResource
                    {
                        ResourceName = name.Value, // Имя
                        Value = value, // Значение
                        Comment = comment != null ? comment.InnerText : "", // Примечание
                        ID = localizable.Count + 1
                    };

                    localizable.Add(newRes);
                }
            }
            return localizable;
        }

        /// <summary>
        ///     Вставить переведенные ресурсы в заранее созданый файл ресурсов
        /// </summary>
        /// <param name="translatedResources">Переведенные ресурсы</param>
        /// <param name="destonationResxFilePath">Путь к файлу ресурсов</param>
        /// <returns> Вернет true, если все ресурсы были встроены в файл ресурсов </returns>
        public static OperationResults TranslateResources(List<StringResource> translatedResources,
            string destonationResxFilePath)
        {
            var result = OperationResults.FileNotFound;
            // Находим файл
            var file = new FileInfo(destonationResxFilePath);
            // Если файл не найдет, то вернем null
            if (file.Exists == false)
                return result;

            //  Загружаем файл
            XmlDocument xmlDocument;
            using (var xmlReader = XmlReader.Create(file.FullName))
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlReader);
            }
            // Получим все элементы с тегом "Data" - ресурсы
            var dataElems = xmlDocument.GetElementsByTagName("data");
            result = OperationResults.AllIsNotTranslated;
            foreach (XmlNode one in dataElems)
            {
                // Если строка действительно локализуемый и это строка
                if (one.Attributes != null)
                {
                    var type = one.Attributes["type"];
                    var name = one.Attributes["name"];
                    var mimetype = one.Attributes["mimetype"];
                    // Если тип не строка(по умолчанию)
                    if (name == null || type != null || mimetype != null) continue;
                    // если имя содержит знаки не локализируемых ресурсов
                    if (name.Value.IndexOf(">>", StringComparison.Ordinal) != -1) continue;

                    var translate = translatedResources.Find(res => res.ResourceName == name.Value);
                    // если среди преводов нет заданного - не все ресурсы встроены
                    if (translate == null)
                    {
                        result = OperationResults.SomeResourceNotTranslated;
                        continue;
                    }
                    var childNode = one.ChildNodes.Cast<XmlNode>().FirstOrDefault(node => node.Name == "value");
                    if (childNode == null) continue;
                    // Если перевод есть, заменяем значение на новое
                    childNode.InnerText = translate.Value;
                    result = OperationResults.TranslateOk;
                }
                xmlDocument.Save(destonationResxFilePath);
            }
            return result;
        }

        /// <summary>
        ///     Вставить переведенные ресурсы в файл-копию оригинального файла ресурсов, именованованный с указаной культурой,
        ///     размещаемую рядом с оригиналом
        /// </summary>
        /// <param name="translatedResources">Переведенные ресурсы</param>
        /// <param name="originalResxFilePath">Путь к исходному файлу ресурсов</param>
        /// <param name="translateCultureInfo">Культура</param>
        /// <returns></returns>
        public static OperationResults TranslateResources(List<StringResource> translatedResources,
            string originalResxFilePath, CultureInfo translateCultureInfo)
        {
            var fInfo = new FileInfo(originalResxFilePath);
            if (fInfo.DirectoryName != null)
            {
                var path = Path.Combine(fInfo.DirectoryName,
                    fInfo.Name.Replace(".resx", "") + "." +
                    translateCultureInfo.TwoLetterISOLanguageName.ToLowerInvariant() + ".resx");
                using (var stream = new FileStream(fInfo.FullName, FileMode.Open))
                {
                    var sr = new StreamReader(stream);
                    var copy = sr.ReadToEnd();
                    var sw = new StreamWriter(path);
                    sw.Write(copy);
                    sw.Close();
                }
                return TranslateResources(translatedResources, path);
            }
            return OperationResults.FileNotFound;
        }
    }
}