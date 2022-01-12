# EPUB Previewer

EPUB Previewer can be used to quickly preview epub files in the default browser. This is useful when sorting e-books, checking for invalid files - or simply just to read a short e-book file.

EPUB Previewer is not meant to be a replacement for a full-featured reader, and takes many shortcuts to quickly open and show the table of contents (TOC) and preview.

## Features

- Supports EPUB 2.0+
- Fast preview in the default browser
- Compiles TOC from multiple sources, to handle bad .epub files (looking at you, Calibre!)
- Ability to add your own CSS
- creates "author - title.epub" filename you can copy to clipboard from preview

## How to use

- Requires .NET 4.8 (included in Windows 10 since May 2019 Update, 11 by defalt. For other versions, download the runtime from [Microsoft offficial page](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)).
- download the file "EpubPreviewer.vY.Z.zip" from [Releases](https://github.com/SanderSade/EpubPreviewer/releases).
- extract EpubPreviewer.exe to an easy-to-access location

In Windows Explorer, right-click on .epub file and choose "Open with" --> "Choose another app", then "More apps" and scroll down until you can select "Look for another app on this PC".

Browse to EpubPreviewer.exe and select it. If you want to, you can also check "Always use this app to open .epub files". 

You can also drag and drop files to the EPUB Previewer window (that is why it stays on top of the other windows).


### Custom CSS support


When creating the preview, program looks for a file named "EpubPreviewer.css" (case-insensitive) in the same folder as EpubPreviewer.exe. If the file is found, it will be included to the preview (added after the default, [EpubPreview.css](https://github.com/SanderSade/EpubPreviewer/tree/master/EpubPreviewer/Epub/Embedded/EpubPreview.css) to the HTML, allowing overriding or enchancing any HTML elements). 

You can see the current HTML and CSS at <https://github.com/SanderSade/EpubPreviewer/tree/master/EpubPreviewer/Epub/Embedded>


## Screenshots (click to enlarge)

Main window:
![Main window](https://user-images.githubusercontent.com/18664267/149160121-7895f3f2-3e77-4f3e-9ac7-7787d710f018.png)


Preview of a book:
![Preview](https://user-images.githubusercontent.com/18664267/149161091-84a25416-237d-4ec6-808d-ab145cf12fe5.png)


## Help wanted!
The preview HTML and CSS could use some touch-up - it is not properly responsive and could look a whole lot better

## Changelog
- 1.0 Everything is new


## Thank you

- my wife asked me to create this, beta tested and gave lots of useful feedback
- EPUB Previewer contains code from [vers-one/EpubReader](https://github.com/vers-one/EpubReader), licensed under [unlicence](https://github.com/vers-one/EpubReader/blob/master/LICENSE), but I would like to thank them anyway!