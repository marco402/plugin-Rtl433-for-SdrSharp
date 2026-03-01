French version after English version.  

##                            1 Installation  
##                            2 Launch  
##                            3 Display  
##                            4 SDRSharp Configuration  
##                            5 Plugin Configuration for RTL_433  
##                            6 File Conversion  
##                            7 Frequencies  
##                            8 Programming  

## 1 Installation  
The location of the plugins is indicated in the SDRSharp.config or SDRSharp.exe.config file, under the core.pluginsDirectory key. This allows you to have a single plugin folder for multiple SDRSharp versions.  
WARNING: 3 different SDRSharp installations.  
Since version 1830, there is no longer a bin folder; simply place the 3 DLLs either in the plugins folder or in plugins/DLL_433. No key is needed.  
For versions where sdrsharp.exe is in a bin folder:  
In this case, navigate to C:\SDRSharp\plugins  
- Create an RTL_433 folder and place the 3 DLLs inside it.  
- Important: Delete the 3 DLLs if they are in bin.  
- Delete the key if it is in plugins.xml.  
For older versions of SDRSharp:  
- Install SDRSharp (tested with version 1.0.0.1788)  
- Place the files from the install folder (SDRSharp.Rtl_433.dll, rtl_433.dll, and GraphLib.dll) into the SDRSharp folder.  
- Add the line `<add key="RTL_433" value="SDRSharp.Rtl_433.Rtl_433_Plugin, SDRSharp .Rtl_433" />` to the plugins.xml file  

## 2 Launch  
After configuring SDRSharp (see the configuration chapter; this information is displayed initially in the plugin window).  
- The SDR play button confirms the sdr_433 plugin start button.  
- The "enabled plugin" button activates the plugin.  
- The "start" button (or "wait" if the radio is off).  
Then wait for a recognized message.  
For more information on Rtl_433, see https://triq.org/rtl_433/OPERATION.html#inputs.  

## 3 Display  
- 3 window types:  
- Message list.  
Displays each device in a separate window containing the list of received messages.  
- "Export data" button: saves received messages to a text file (.txt).  
- Graph.  
Displays each device in a separate window containing three graphs: IQ, AM or FM, and pulses.  
This includes data from the last four messages.  
- 'Record one shot' button: records IQ data to a .wav file (2 channels: I and Q).  
- List devices: Displays the list of received devices.  
- Option to save data to the devices.txt file when closing the window.  
- Option to reload the devices.txt file when opening the window.  

## 4 SDRSharp Configuration  
- The plugin uses raw IQ data; only the source parameters affect its operation.  

Parameters Configure Source  
- Sampling Mode -> Quadrature Sampling  
- Sample Rate -> 0.25 MSPS or more for certain devices with FSK or f > 433 MHz...  
- AGC: On (corresponds to auto gain with RTL433) can be manually turned off.  
- RTL AGC: On (not the AGC panel) can be turned off if there are good signals.  
- Check Frequency  

## 5 Plugin Configuration for RTL_433  
- In addition to the frequency and sample rate:  
- Data Conv(-C) changes the units for displaying messages.  
- save(-S) records received streams in cu8 format.  
- Hide select prevents the display/saving of devices selected in the list. hide show devices(-R).  
- Show select displays/saves only the selected devices.  
- To populate the device list, start the program once.  
- The RTL_433 options are in parentheses.  

## 6 File Conversion  
- Converts .cu8 files to 2-channel (I and Q) .wav files for reloading with SDRSharp.  
- The sampling rate is taken from the end of the filename, between underscores (_) and underscores (k).  
- If this does not exist, 250000 is taken by default.  

## 7 Frequencies  
- It is possible to select the different frequencies listed on the website:  
https://triq.org/rtl_433/#building-installation  
- Selecting "free" allows you to launch the plugin without changing the frequency.  

## 8 Programming  
2 options:  
-1. SDRSharpMini.sln: This project generates the 3 DLLs mentioned for the installation above.  
You will first need to download:  
- The GraphLib project: https://github.com/marco402/GraphLibSpecific  
- The Rtl_433_dll project: https://github.com/marco402/Rtl_433_dll-for-plugin-SdrSharp  
- SDRSharp (tested with version 1.0.0.1788).  
Update the download, compilation, and reference paths as needed.  

-2. SDRSharp.sln: In addition to the light version, download the original SDRSharp project on GitHub: https://github.com/SDRSharpR/SDRSharp  

## 1 Installation  
L'emplacement des plugins est indiqué dans le fichier SDRSharp.config ou SDRSharp.exe.config,sous la cle core.pluginsDirectory.Il est ainsi possible d'avoir 1 seul dossier plugin pour plusieurs version SDRSharp.  
ATTENTION:3 installations de SDRSharp différentes.  
Depuis la version 1830, il n'y a plus de dossier bin, il suffit de mettre les 3 dll soit dans le dossier plugins soit dans plugins/DLL_433 pas besoin de cle.  
Pour les versions qui ont sdrsharp.exe dans un dossier bin.  
Dans ce cas, se positionner dans C:\SDRSharp\plugins  
    - Créer un dossier RTL_433 et y placer les 3 dll.  
    - Important:Supprimer les 3 dll si elles sont dans bin.  
    - Supprimer la cle si elle est dans plugins.xml.  

Pour les version SDRSharp plus anciennes:  
- Installation de SDRSharp(testé avec la version 1.0.0.1788)  
  - Placer les fichiers du dossier install (SDRSharp.Rtl_433.dll rtl_433.dll et GraphLib.dll) dans le dossier SDRSharp.  
  - Ajouter la ligne <add key="RTL_433" value="SDRSharp.Rtl_433.Rtl_433_Plugin, SDRSharp .Rtl_433" /> dans le fichier plugins.xml  

## 2 Lancement  
Après avoir configuré SDRSharp (voir chapitre configuration, ces informations sont rappelées au départ dans la fenêtre du plugin).  
    - Le bouton play de SDR valide le bouton start du plugin sdr_433.  
    - Bouton enabled plugin pour activer le plugin.  
    - Bouton start (wait si la radio est arretee).  
Ensuite patienter en attendant un message reconnu.  
Pour davantage d'informations sur Rtl_433 voir https://triq.org/rtl_433/OPERATION.html#inputs.  

## 3 Affichage  
    -3 types de fenêtre:  
        - List messages.  
            Affichage de chaque device dans une fenêtre differente contenant la liste des messages reçus.  
            -bouton 'export data':enregistre les messages reçus dans un fichier texte(.txt).  
        - Graph.  
            Affichage de chaque device dans une fenêtre differente contenant 3 graphiques IQ,AM ou FM et pulses.  
                avec les données des 4 derniers messages.  
                -Bouton 'record one shoot':enregistre les données IQ dans un fichier .wav 2 canaux I et Q .  
        - List devices  
            Affichage de la liste des devices recus.  
                -Possibilité d'enregistrer les données dans le fichier devices.txt à la fermeture de la fenêtre.  
                -Possibilité de recharger le fichier devices.txt à l'ouverture de la fenêtre.  

## 4 Paramétrage de sdrSharp  
 Le plugin utilise les données brutes IQ, seul le parameters de la source influent sur le fonctionnement.  

  - Parameters configure source  
    - Sampling mode->quadrature sampling  
    - Sample Rate->0.25 MSPS or more for certain devices FSK or f > 433Mhz...  
    - AGC:on(corresponds to auto gain with rtl433) can be manually->off.  
    - RTL AGC:on.(not the AGC panel) can be set off if good signals.  
    - Check frequency  

## 5 Partie Paramétrage du plugin pour RTL_433  
    - En plus de la fréquence et du sample rate  
    - Data Conv(-C) change les unitées pour l'affichage des messages.  
    - save(-S) enregistrement des flux reçus format cu8  
    - Hide select permet de ne pas afficher /enregistrer les devices sélectionnés dans la liste hide show devices(-R).  
    - Show select permet d'afficher /enregistrer que les devices sélectionnés.  
    - Pour remplir la liste des devices, start une première fois.  
    - Entre parenthèse, les optionsRTL_433.  
 
## 6 Conversion de fichier  
    - Conversion de fichier .cu8 vers des fichiers .wav 2 canaux I et Q pour les recharger avec SDRSharp.  
    - La vitesse d'échantillonnage est prélevée à la fin du nom du fichier entre _ et k.  
    - Si celle-ci n'existe pas, 250000 est prise par défaut.  

## 7 Fréquences  
    - Il est possible de sélectionner les différentes fréquences citées sur le site  
    https://triq.org/rtl_433/#building-installation  
    - La sélection free permet de lancer le plugin sans changer la fréquence.  

## 8 Programmation  
2 possibilités:
    -1. SDRSharpMini.sln: Ce projet permet de générer les 3 dll citées pour l'installation ci-dessus.  
       Il faudra auparavant télécharger:  
       - Le projet GraphLib: https://github.com/marco402/GraphLibSpecific  
       - Le projet Rtl_433_dll: https://github.com/marco402/Rtl_433_dll-for-plugin-SdrSharp  
       - SDRSharp (testé avec la version 1.0.0.1788).  
       Mettre à jour si nécessaire les chemin de chargement, de compilation et des références.  
    -2. SDRSharp.sln: En plus de la version light, télécharger le projet d'origine SDRSharp sur Github: https://github.com/SDRSharpR/SDRSharp  

