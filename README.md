# CEurope
![I](https://img.shields.io/github/release/0leXis/CEurope.svg) ![II](https://img.shields.io/github/issues/0leXis/CEurope.svg) ![III](https://img.shields.io/github/downloads/0leXis/CEurope/total.svg) ![IIII](https://img.shields.io/github/license/0leXis/CEurope.svg?style=popout)
> Внимание!!!
> Данный язык был создан чуть больше, чем за 1 неделю. В связи с этим он может содержать огромное кол-во багов,
> нечитаемый код и странные проектные решения. Автор не разбирается в создании компиляторов и интерпретаторов и
> не рекомендует использование данного языка в реальных проектах.

## CEVM
CEVM - интерпретатор CEurope. Код CEVM расположен в проекте CEVirtualMachine. 
CEVM исполняет скрипты CEurope и поддерживает следующие аргументы командной строки:
* -script <путь к файлу> - выполняет указанный скрипт.
* -dtime - по окончании выполнения скрипта выводит время, за которое он был выполнен.
* -out <путь к файлу> - по окончании выполнения скрипта консоль автоматически закрывается, а вся выходная информация записывается в файл.
## CEurope IDE
CEurope IDE - легкая среда разработки с подсветкой синтаксиса. Позволяет редактировать скрипты формата ".ce". 
Содержит 2 области: редактор кода и окно интерпретации (вывод перенаправляется в данное окно после завершения скрипта).

![IDE](https://github.com/0leXis/CEurope/blob/master/Screenshots/IDE.png)

## Примеры и документация

Документация расположена на [вики проекта](https://github.com/0leXis/CEurope/wiki)
Примеры расположены в папке [Samples](https://github.com/0leXis/CEurope/tree/master/Samples).
