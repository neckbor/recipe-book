# Веб-приложение YummyYummy

*[YummYummY/Ссылка на приложение](http://195.133.196.144/yummy/)*

*[Видеопоказ/Ссылка на видео](https://www.youtube.com/watch?v=IcoHOXVD0CQ&feature=youtu.be)*

*[Видео-презентация/Ссылка на видео](https://youtu.be/r92gtvGY9Mw)*

*[Swagger/Ссылка на Swagger](http://195.133.196.144/index.html)*

### Техническое задание
*[Github/Техническое задание.docx](https://github.com/neckbor/recipe-book/blob/master/%D0%B4%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D0%B0%D1%86%D0%B8%D1%8F/%D0%A2%D0%B5%D1%85%D0%BD%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%BE%D0%B5%20%D0%B7%D0%B0%D0%B4%D0%B0%D0%BD%D0%B8%D0%B5.docx)*

*[Github/Техническое задание.pdf](https://github.com/neckbor/recipe-book/blob/master/%D0%B4%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D0%B0%D1%86%D0%B8%D1%8F/%D0%A2%D0%B5%D1%85%D0%BD%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%BE%D0%B5%20%D0%B7%D0%B0%D0%B4%D0%B0%D0%BD%D0%B8%D0%B5.pdf)*

### Курсовой проект
*[Github/Курсовой проект.docx](https://github.com/neckbor/recipe-book/blob/master/%D0%B4%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D0%B0%D1%86%D0%B8%D1%8F/%D0%9A%D1%83%D1%80%D1%81%D0%BE%D0%B2%D0%BE%D0%B9%20%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82.docx)*

*[Github/Курсовой проект.pdf](https://github.com/neckbor/recipe-book/blob/master/%D0%B4%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D0%B0%D1%86%D0%B8%D1%8F/%D0%9A%D1%83%D1%80%D1%81%D0%BE%D0%B2%D0%BE%D0%B9%20%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82.pdf)*

### Отчетный документ по ролям
*[Github/Отчетный документ по ролям.docx](https://github.com/neckbor/recipe-book/blob/master/%D0%B4%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D0%B0%D1%86%D0%B8%D1%8F/%D0%9E%D1%82%D1%87%D1%91%D1%82%D0%BD%D1%8B%D0%B9%20%D0%B4%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%20%D0%BF%D0%BE%20%D1%80%D0%BE%D0%BB%D1%8F%D0%BC.docx)*

*[Github/Отчетный документ по ролям.pdf](https://github.com/neckbor/recipe-book/blob/master/%D0%B4%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D0%B0%D1%86%D0%B8%D1%8F/%D0%9E%D1%82%D1%87%D1%91%D1%82%D0%BD%D1%8B%D0%B9%20%D0%B4%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%20%D0%BF%D0%BE%20%D1%80%D0%BE%D0%BB%D1%8F%D0%BC.pdf)*

### Презентация
*[Github/YummYummY.pptx](https://github.com/neckbor/recipe-book/blob/master/%D0%B4%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D0%B0%D1%86%D0%B8%D1%8F/YummYummY.pptx)*

*[YouTube/Видео-презентация](https://youtu.be/r92gtvGY9Mw)*

### Дополнительные ресурсы ###
* ##### [Trello](https://trello.com/b/Ql54ikNJ/yummyummy)
* ##### [Miro](https://miro.com/app/board/o9J_kul6-84=/)

## Краткое описание проекта
Данный проект предполагает создание сервиса для просмотра рецептов блюд. Расположение в Глобальной сети Интернет позволит каждому пользователю использовать это приложение.

Приложение предоставляет **пошаговый** просмотр рецептов блюд.

Приложение имеет разделение пользователей по правам:
1. Неавторизованный пользователь
2. Авторизованный открытый пользователь
3. Авторизованный заблокированный пользователь
4. Администратор

В зависимости от типа пользователя имеются возможности:
* Неавторизованный пользователь имеет возможность просмотра рецептов блюд, поиска по ним.
* Авторизованный пользователь имеет все возможности неавторизованного пользователя, а так же может добавлять, изменять и удалять собственные рецепты.
* Администратор имеет возможности, описанные выше, а так же может заблокировать авторизованного пользователя, если тот проявляет активность, которую администратор сочтёт недопустимой. В таком случае у авторизованного пользователя пропадут возможности добавления и изменения собственных рецептов.
***

### Участники проекта ###
Бакулин Александр группа 1.1 

Почта для связи: 	bakulin.alexxx@gmail.com

Бородин Никита группа 1.1

Почта для связи:  neckbor@mail.ru

Пупыкин Алексей группа 1.1

Почта для связи:  mymailvrn98@gmail.com
