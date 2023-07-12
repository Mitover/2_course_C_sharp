# 1 лабораторная работа
На основе одной из готовых обобщенных (шаблонных) объектных коллекций .NET создать класс «Управляющая компания», включающий список объектов недвижимости (строений). Классы строений должны образовывать иерархию с базовым классом. Объекты недвижимости бывают двух типов: жилые и нежилые. Описать в базовом классе абстрактный метод для расчета приближенного среднего количества жильцов/работников строения. Для жилых построек среднее количество жильцов – это количество квартир на количество комнат в квартире (тип квартиры) на 1.3, для нежилых строений среднее количество сотрудников пропорционально площади с коэффициентом 0.2. В виде меню программы реализовать нижеприведенный функционал.

1. Упорядочить всю последовательность объектов недвижимости по убыванию среднего 
количества жильцов/работников. При совпадении значения – упорядочивать данные по типу 
строения (жилые, нежилые), затем по алфавиту по адресу строения. Вывести тип строения, адрес 
строения, среднее количество жильцов/работников для всех элементов списка.
2. Вывести первые 3 объекта из полученного в пункте 1 списка.
3. Вывести последние 4 адреса объекта из полученного в пункте 1 списка.
4. В реальном времени (в процессе заполнения списка строений) рассчитывать и поддерживать в 
актуальном состоянии среднее количество жильцов/работников объекта недвижимости по 
компании в целом, сохранить значение как поле класса «Управляющая компания».
5. Организовать запись и чтение всех данных в/из файла. Реализовать поддержку 2-х форматов 
файлов: XML и JSON. Использовать либо стандартные средства (форматтеры и сериализаторы), 
либо собственные.
6. Организовать обработку некорректного формата входного файла.

# 2 Лабораторная работа
Анализ должен осуществляться по всем страницам, URI которых включает базовый URI ресурса (интернет-домен, например, www.susu.ru). Предусмотреть настройку максимального уровня вложенности страниц в рекурсивном алгоритме анализа, а также максимального количества просматриваемых страниц. В класс добавить событие (event) по определению цели поиска, с передачей в его обработчик информации о названии ссылки, ведущей на страницу (т.е. имя ссылки, которое видит  пользователь на базовой странице в браузере, например, «Филиалы»), URI страницы (например, http://www.susu.ru/ru/university/old-departments/branches), уровне вложенности (например, 1) и  самой цели поиска (см. разбивку задания по вариантам). Если целей на странице несколько, то событие вызывается для каждой цели. Обработчик события должен выводить информацию на  консоль (или в окно) и в CSV-файл в табличной форме. CSV-файл можно открыть в Excel. Применение событийной модели позволит отделить друг от друга алгоритм поиска данных на  страницах и алгоритм отображения/вывода информации на консоль/в файл.

_Цель поиска – контактная информация (адрес, телефон)_

# 3 Лабораторная работа
Разработать программу-клиент для социальной сети (на выбор студента). Программа должна представлять многооконное приложение (минимум 2 окна). Функциональность и графический интерфейс на усмотрение студента. Взаимодействие с социальной сетью через REST API. Предусмотреть минимум 3 разнотипных запроса к социальной сети через REST API
