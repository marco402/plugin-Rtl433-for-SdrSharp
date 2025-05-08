## New Version  
Version 1.5.8.4 May 25, 2025.  
    -Acceleration of the loading of the graphic window.  
Version 1.5.8.3 April 29, 2025.  
-A user told me a problem with the Bresser device at 868Mhz and sample rate 1000k.  
    -With the replay he gave me, one of the 2 carriers has a minimum period of 5.74μs which
requires a minimum sample rate of 350k.  
To solve this problem, I removed the decimation which systematically decimated at 250k.  

Version 1.5.8.0 February 27, 2025.  
    -Devices window.  
        -Synchronization of the 3 graphs. Pulses, Am or Fm and IQ.  
        -Ability to validate the recording of the next message on all device windows
            compared to only 1 previously.  
    -Normalized wav file names for SDRSharp.  
        Frequency and sample rate.  
    -Removed the choice of MONO or STEREO, recordings from the device window are files
        wav has 2 channels I and Q.  
    -Removed the verbose choice.  
    -Removed the choice enabled devices disabled.  
    -Removed metadata:always displayed.  
    -Decimation IQ of sampleRate/250000, prefer a sampleRate of 250000, less time and less memory.  
        The theoretical lower limit is 200000.  
    -Updated devices RTL433 version 24.10 (2024-10-30): 217--->238 devices.  
    -Changed RTL_433 for 64bits.  

## Installation  
For all versions, the location of the plugins is indicated in the SDRSharp.config or SDRSharp file.exe.config,
under the key core.pluginsDirectory.Il is thus possible to have 1 single plugin folder for several SDRSharp versions.  

​Warning:3 differents SDRSharp installations.  
Since version 1830, there is no longer a bin folder, just put the 3 dll either in the plugins folder
either in plugins/DLL_433 no need for key.
For versions that have sdrsharp.exe in a bin folder.  
In this case, position yourself in C:SDRSharp/plugins  
    - Create a folder RTL_433 and place the 3 dlls there.  
    - Important:Delete the 3 dlls if they are in bin.  
    - Delete the key if it is in plugins.xml.  

For older SDRSharp versions:  
- Installation of SDRSharp (tested with version 1.0.0.1788)  
  - Place the files from the install folder (SDRSharp.Rtl_433.dll rtl_433.dll and GraphLib.dll) in the SDRSharp folder.  
  - Add the line <add key="RTL_433" value="SDRSharp.Rtl_433.Rtl_433_Plugin, SDRSharp .Rtl_433" /> in the plugins file.xml  

## Launch
After configuring SDRSharp (see chapter configuration, this information is initially recalled in the plugin window).  
The play button of SDR validates the start button of the sdr_433 plugin.  
Enabled plugin button to activate the plugin.  
Then start button(wait if radio is stopped)  
Then wait for a recognized message.  
For more details on rtl_433 see https://triq.org/rtl_433/OPERATION.html#inputs.  

# Display  
    -3 types of window:
        -List messages.
            Display of each device in a different window containing the list of messages received.
            -'Export data' button: saves the received messages in a text file (.txt).
        -Graph.
            Display of each device in a different window containing 3 IQ, AM or FM and pulse graphs.
               with the data of the last 4 messages.
            -'Record one shoot' button: saves IQ data to a 2-channel I and Q .wav file.
        -List devices
            Display of the list of devices received.
                -Possibility to save the data in the devices.txt file when the window is closed.
                -Possibility to reload the devices.txt file when the window opens.

# Setting up sdrSharp  
The plugin processes the raw IQ data.  
Configure source button:  
    - Sampling mode:quadrature sampling  
    - Sample Rate:0.25 MSPS (value of rtl433)  
    - RTL AGC:on.(not the AGC panel)  
    - Tuner AGC:on.  
    - Frequency.   

# File Conversion  
    - Convert .cu8 to .wav STEREO files to reload with SDRSharp.  
    - The sampling rate is taken at the end of the file name between _ and k.  
    - If it does not exist, 250,000 is taken by default.  

# Frequency  
    It is possible to select the different frequencies quoted on the site  
    https://triq.org/rtl_433/#building-installation  
The free selection allows to launch the plugin without changing the frequency.  

## Previous versions  
Version 1.5.7.0 May 8, 2024.  
    -Removing the console RTL_433.  
    -The information is transferred to the text box at the bottom of the plugin.  
    -Modification to accommodate theme changes (SDRSharp 1920).  
  
French version after English version.  
    You can display dump windows on https://marco40github.wixsite.com/website/plugin-sdrsharp-pour-rtl-433?lang=en  
Version Francaise après la version Anglaise.  
    Vous pouvez visualiser des dump fenêtres sur https://marco40github.wixsite.com/website/plugin-sdrsharp-pour-rtl-433  
	
Version 1.5.6.4 March 31, 2024 Display console only if verbose.
Version 1.5.6.3 march 26, 2024 Tested with SDRSharp 1919 and 1920 beta.  
    -Consideration of themes sdrsharp(only Font,ForeColor,BackColor and Cursor).  
Version 1.5.6.2 February 16, 2024 Tested with SDRSharp 1919.  
    -Update RTL_433 (25 more devices).  
Version 1.5.6.1 January 23, 2023 Tested with SDRSharp 1906.  
    -Added recording of all received messages in text files.  
       -Validation by the Record check box.  
       -Only for List messages windows.  
       -File name = date_time_device name(+channel).txt.  
       -You can limit the devices in the list of devices by checking Show select.  
       -You can create the Recorders folder at SDRSharp.exe.  
       -Be careful not to saturate the disk.  
       -The data is aligned with the first line received.  
       -The data is separated by a tab for reloading into Open-Office.  
       -If you uncheck Record, close the windows to stop recording.  
       -The files are closed when the windows are closed.  
       -You can close all windows by disabling the plugin.  
Version 1.5.6.0 January 11, 2023 Tested with SDRSharp 1906.  
    -Update RTL_433 (25 more devices).  
    -Change framework 4.6 to 4.8. 
Version 1.5.5.0 February 17, 2022 Tested with SDRSharp 1854.  
     -Added an Enabled checkbox for cohabitation of the RTL433 plugin with the other plugins.  
     -Display of the time required to process the data functions of the sampleRate/s in milliseconds (Cycle time),
          it changes to red if it is greater than 1000(Not if source is file).  
     -Display of the processing time per second taken by the software RTL_433 in milliseconds (Time RTL433).  
Version 1.5.4.4  17 Janvier 2022  
     - Minor changes for Honeywell_cm921.  
          -boiler_modulation_level.  
          -ticker command.  
     - Correction crash when closed window listDevices if SDRSharp framework 6.0 since version SDRSharp 1830.  
     - 100ms timeout in replay mode (source iq file(.wav)).Timer increased to 1000ms from V1.5.5.0  
Version 1.5.4.3  12 Janvier 2022  
     - Added ID for identify devices.->suppression from V1.5.5.0 problem for TPMS left IDS for Honeywell 921.  
Version 1.5.4.2 10 January 2022  
     -Fix listMessage window on receiving message of different length.  
Version 1.5.4.1 9 January 2022  
     -Fix listMessage window on receiving message of different length. Added 10 columns instead of 2 if -MLevel.  
     -To see to add columns being received.  
Version 1.5.4.0 6 January 2022  
     -Add directory dll 1.5.4.0 specific for SDRSharp 1777 or 1784.  
Version 1.5.4.0 5 January 2022  
     -Update of version RTL_433(V21.12 of December 29,2021,the previous one was dated March 1,2021).  
           -Added 27 devices(default/default+disabled/all)(150/174/180)-->(177/202/208).  
     -Added a column number of messages received in the list devices window.  

Version 1.5.0.0 1 August 2021  
  - Modifications  
      -Add a third radio button under the start button to display a new window type:  
         Display of one window per device that contains all messages received up to 10 times the maximum number of devices set in . config * 10.  
          100*10 by default.  
         The export button allows to save the data in text format which can be reloaded by a spreadsheet(calc... separator:tabulation).  
         The name of the file is that of the window.CAUTION overwriting the file without confirmation.  
         If the Recordings folder of SDRSharp exists, the files will be saved in it otherwise in the SDRSharp folder.  
Version 1.5.1.0  
		Window list message display last message at the top.  
Version 1.5.2.0  
		Export First message at the first position like 1.5.0.0.  
Version 1.5.3.0  
  -Modifications  
      -Messages list window
          Index error when signal is weak(I failed to reproduce it).  
          Addition test on the number of columns to follow...
          Added 2 additional columns, to see if the problem comes from a bad column header which will display it.  
          This shouldn’t be happening, the names of the headers are in "hard" in code RTL_433?  
		  
      -Change of SDRsharp reference for compilation:  
          The plugin_rtl433 with sdrsharp-x86-dotnet4(1784) or 1777 version only works with the version used as reference(1632) in visual studio.  
          By referencing the sdrsharp-x86-dotnet4(1784) version, the plugin works on the 1784 but also on the 1777...  
Version 1.5.3.1  
  -Modifications  
        -Added a checkbox in the "enabled devices disabled" frame to process disabled devices(.disabled = 1 in devices files)  

Version 1.4.1.0  7 July 2021
  - Modifications  
      Keep the console open on stop plugin for read verbose informations(version RTL_433 1.3.2.0)  
  - Corrections  
      Crash call function cu8 to .wav.  
  - Modifications configuration on TUNER AGC and RTL AGC  
      Tuner AGC:on(corresponds to auto gain with rtl433) can be manually-> off.\n")  
      RTL AGC:on.(not the AGC panel) can be set off if good signals.\n")  
Version 1.4.0.0  July 2021  
  - Developments  
     - Added 2 radio buttons under start button.  
        - Graph:opening a window per device (same as previous versions).  
        - List devices:Display a single list containing one line per device with the last data received (request from an Internet user).  
        - Columns are added when receiving new data labels.  
        - When this window is closed, the data can be saved in text format that can be refilled by a spreadsheet(calc.. separator:tabulation).  
          The file is called devices.txt, CAUTION overwriting the file without confirmation.  
        - When selecting this type of window (List devices button), it is possible to reload the file devices.txt.  
          This window is limited to 100 columns and 10 times the maximum number of devices set in . config * 10 (100*10 by default).  
        It is possible to switch from one mode to another without stopping the plugin.  
    - Add a radio button above the device list.  
        - Hide select:no processing of selected devices (same as previous versions).  
        - Show select:processing only selected devices.  
  - Corrections  
        - Crash on a second call of . cu8 to .wav.  
        - Time correction on charts that did not take sample rate into account.  
Version 1.3.1.0 8 June 2021  
  - Modifications  
       The sample rate > 250000 is managed, 250000 is still preferable,use lowest sample rate.  
         Recording at device windows is only allowed at 250,000.  
         This amendment was a request to be able to use Airspy mini that does not go down to 250000.  
  - Corrections  
       With -vvv, a window "zombi could be displayed due to received messages.  
Version 1.3.0.0 June 2021  
  - Developments  
      - Loading the list of devices and forcing the frequency SDRSharp at the first start of the plugin and not at loading.  
      - Added messageBox if problem with loading.  
      - Memorize Metadata option.  
      - Memorize Frequency option.  
  - Changes
      - To limit memory consumption, the graphs are displayed on the first 5 and on demand for others (Display curves button on windows),
          this limit is stored in SDRSharp.config or SDRSharp.exe.config depending on the versions after a first correct loading of the plugin.  
          key is RTL_433_plugin.nbDevicesWithGraph.  
      - Limiting the devices window limit to 100, this limit is stored in the SDRSharp.config file or SDRSharp.exe.config depending on the
          version after a correct first load of the plugin. RTL_433_plugin.MaxDevicesWindows key (can be lowered if posts still have memory 
          problems without graphics).  
      - RTL_433 options are displayed on the plugin message window.
  - Informations
      - If you used a previous version of the plugin, you can erase all 3 keys in the file SDRSharp.config or SDRSharp.exe.config:
          DataConvNative DataConvSI and DataConvNativeCustomary that I replaced with a single line RTL_433_plugin.DataConv.  
      - If the SDRSharp source is on RTL SDR-TCP, a setting that works:
        - Open SDRSharp  
        - Select the RTL-SDR TCP source.  
        - Configure source:  
        - Host  
        - Port  
        - Sample rate=0.25MSPS  
        - Select RTL AGC  
        - Start SDRSharp  
        - Select Tuner AGC  
        - Start the plugin.  
  - Testing  
      - I testing on SDRSharp Github 1632 release in development.  
      - I validate on the latest SDRSharp (1811) version on Windows 10 64bits.

Version 1.11  May 2- Corrections
- cu8 to wav. --> reminder:cu8(-S) and MONO and STEREO (cu8 to wav) files are located at the SDRSharp exe.  
The MONO and STEREO files generated from the device window are located in Recordings if it exists otherwise
at the SDRSharp exe.  
- MONO record.021  

  - Developments  
    - Added -C data conv option.  
    - The device window displays the last 4 messages.  
    I added these 2 options for TPMS messages for:  
        - display pressure in kilo-pascal and temperature in degrees (SI option).  
        - and to obtain the pressures of the 4 tyres.  
    - Added pulse data curves to FSK.  
  - Changes  
        - Resumption of console management for cohabitation with other plugin with console (DSDPlus...)  
  - Corrections  
        - Fix to be able to replay the files generated by one shoot record (the 50000 samples to decrease
          the cycle time was not enough (recording the last 5 buffer of 50000).  
  - Informations  
        - The number of device windows is not limited by the code, the limitation is related to memory
          (order of magnitude for 130 windows : 1G of memory)  
        - The plugin works with AirSpy R2(https://www.rtl-sdr.com/rtl433-plugin-for-sdr-now-available/#comments).  

Version 1.10  April 2021
    - Added  pulses data, analysis and fm graphics.  
        note: analysis and fm are synchronous(same sampling period).  
            The 3 graphs are time synchronized with the first sample.  
    - Added save -S option. 
            This version saves the . cu8 files in the SDRSharp.exe folder.  
    - Added list devices for option -R.  
      - Select no display devices.  
    - Add file test in install\Recordings for replay with input SDRSharp(Baseband Files(*))  

Version 1.00  March 2021

# Test configuration  
    - Operating systeme:Windows 10.  
    - Clé DVB-T+FM+DAB 820T2 & SDR DV3 USB2.0  
    - Modified version of rtl_433 of March 1, 2021.  

## Programming  
2 possibilities:  
    1. - SDRSharp_light.sln: This project generates the 3 dll mentioned for the above installation.  
      You will need to download:  
      - GraphLib Project: https://github.com/marco402/GraphLibSpecific  
      - Rtl_433_dll: https://github.com/marco402/Rtl_433_dll-for-plugin-SdrSharp
      - SDRSharp (tested with version 1.0.0.1788).  
      Update the load , build and references paths .  
    2. - SDRSharp.sln: In addition to the light version, download the original SDRSharp project on Github: https://github.com/SDRSharpR/SDRSharp  
________________________________________________________________________________________________________________________________________________  
## Nouvelle version  
Version 1.5.8.4 25 Mai 2025  
    -Accéleration du chargement de la fenetre graph.  
Version 1.5.8.3 29 Avril 2025  
    -Un utilisateur m'a indiqué un problème avec le device Bresser à 868Mhz et sample rate 1000k.  
       -Avec le replay qu'il m'a procuré, une des 2 porteuses à une périod minimum de 5.74µs ce qui
demande un sample rate minimum de 350k.  
    Pour regler ce problème, j'ai supprimé la décimation qui décimait systématiquement à 250k.  

Version 1.5.8.0 20 Février 2025  
    -Fenetre devices.  
        -Synchronisation des 3 graphiques.Pulses,Am ou Fm et IQ.  
        -Possibilité de valider l'enregistrement du prochain message sur toutes les fenetres devices
            contre 1 seule precedemment.  
    -Normalisation du nom des fichiers wav pour SDRSharp.  
        -frequence et sample rate.  

    -Suppression du choix MONO ou STEREO, les enregistrements a partir de la fenetre device sont des fichier
        wav a 2 canaux I et Q.  
    -Suppression du choix verbose.  
    -Suppression du choix enabled devices disabled.  
	-Supression de metadata:toujours affichées.  
    -Decimation IQ de sampleRate/250000, preferer un sampleRate de 250000, moins de temps et moins de mémoire.
        La limite theorique basse est de 200000.  
    -Mise a jour des devices RTL433 version 24.10 (2024-10-30): 217--->238 devices.  
	-Modification de RTL_433 pour 64bits.  

## Installation  
​Pour toutes les version, l'emplacement des plugins est indiqué dans le fichier SDRSharp.config ou SDRSharp.exe.config,
sous la cle core.pluginsDirectory.Il est ainsi possible d'avoir 1 seul dossier plugin pour plusieurs version SDRSharp.
ATTENTION:3 installations de SDRSharp différentes.  
Depuis la version 1830, il n'y a plus de dossier bin, il suffit de mettre les 3 dll soit dans le dossier plugins
soit dans plugins/DLL_433 pas besoin de cle.
Pour les versions qui ont sdrsharp.exe dans un dossier bin.  
Dans ce cas, se positionner dans C:\SDRSharp\plugins  
    - Créer un dossier RTL_433 et y placer les 3 dll.  
    - Important:Supprimer les 3 dll si elles sont dans bin.  
    - Supprimer la cle si elle est dans plugins.xml.  

Pour les version SDRSharp plus anciennes:  
- Installation de SDRSharp(testé avec la version 1.0.0.1788)  
  - Placer les fichiers du dossier install (SDRSharp.Rtl_433.dll rtl_433.dll et GraphLib.dll) dans le dossier SDRSharp.  
  - Ajouter la ligne <add key="RTL_433" value="SDRSharp.Rtl_433.Rtl_433_Plugin, SDRSharp .Rtl_433" /> dans le fichier plugins.xml  

## Lancement
Après avoir configuré SDRSharp (voir chapitre configuration, ces informations sont rappelées au départ dans la fenêtre du plugin).  
    -Le bouton play de SDR valide le bouton start du plugin sdr_433.  
    -Bouton enabled plugin pour activer le plugin.  
    -Bouton start (wait si la radio est arretee).  
Ensuite patienter en attendant un message reconnu.  
Pour davantage d'informations sur Rtl_433 voir https://triq.org/rtl_433/OPERATION.html#inputs.  


# Affichage  
    -3 types de fenêtre:  
        -List messages.  
            Affichage de chaque device dans une fenêtre differente contenant la liste des messages reçus.  
            -bouton 'export data':enregistre les messages reçus dans un fichier texte(.txt).  
        -Graph.  
            Affichage de chaque device dans une fenêtre differente contenant 3 graphiques IQ,AM ou FM et pulses.
                avec les données des 4 derniers messages.
                -Bouton 'record one shoot':enregistre les données IQ dans un fichier .wav 2 canaux I et Q .  
        -List devices  
            Affichage de la liste des devices recus.  
                -Possibilité d'enregistrer les données dans le fichier devices.txt à la fermeture de la fenêtre.  
                -Possibilité de recharger le fichier devices.txt à l'ouverture de la fenêtre.  

# Paramétrage de sdrSharp  
    -Le plugin traite les données brutes IQ.  

# Bouton configure source:  
    -Sampling mode:quadrature sampling  
    -Sample Rate:0.25 MSPS (valeur de rtl433)  
    -RTL AGC:on.(pas le panel AGC)  
    -Tuner AGC:on.  
    -Fréquence.  

# Conversion de fichier  
    - Conversion de fichier .cu8 vers des fichiers .wav 2 canaux I et Q pour les recharger avec SDRSharp.  
    - La vitesse d'échantillonnage est prélevée à la fin du nom du fichier entre _ et k.  
    - Si celle-ci n'existe pas, 250000 est prise par défaut.  

# Fréquences  
    -Il est possible de sélectionner les différentes fréquences citées sur le site  
    https://triq.org/rtl_433/#building-installation  
    -La sélection free permet de lancer le plugin sans changer la fréquence.  

## Versions précédentes
Version 1.5.7.0 8 Mai 2024  
    -Supression de la console RTL_433.  
    -Les informations sont transférées dans la zone de texte en bas du plugin.  
    -Modification pour s'adapter aux changements de theme(SDRSharp 1920)  
    
Version 1.5.6.4 31 Mars 2024 Affichage de la console uniquement si verbose.
Version 1.5.6.3 26 Mars 2024 Testé avec SDRSharp 1919 et 1920 beta.  
    -Prise en compte des thèmes sdrsharp(only Font,ForeColor,BackColor and Cursor).  
Version 1.5.6.2 16 Février 2024 Testé vec SDRSharp 1919.  
    -Update RTL_433 (25 more devices).  
Version 1.5.6.1 23 janvier 2023 Testé avec SDRSharp 1906.  
    -Ajout de l'enregistrement de tous les messages reçus dans des fichiers texte.  
       -Validation par la case à cocher Record.  
       -Uniquement pour les fenêtres List messages.  
       -Nom des fichiers = date_time_name du périphérique(+canal).txt.  
       -Vous pouvez limiter les devices dans la liste des devices en cochant Show select.  
       -Vous pouvez créer le dossier Recorders au niveau de SDRSharp.exe.  
       -Attention de ne pas saturer le disque.  
       -Les données sont alignées sur la première ligne reçue.  
       -Les données sont séparées par une tabulation pour rechargement dans un classeur Open-Office.  
       -Si vous décochez Record, il faut fermer les fenêtre pour arrêter l'enregistrement.  
       -Les fichiers sont fermés à la fermeture des fenêtres.  
       -Vous pouvez fermer toutes les fenêtres en désactivant le plugin.  
Version 1.5.6.0 11 janvier 2023 Testé avec SDRSharp 1906.  
    -Mise à jour RTL_433 (25 devices supplémentaires).  
    -Passage du framework 4.6 a 4.8.  
Version 1.5.5.0  17 Février 2022   Testé avec SDRSharp 1854.  
     -Ajout d'une case a cocher Enabled pour cohabitation du plugin RTL433 avec les autres plugins.  
     -Affichage du temps nécessaire pour traiter les données fonctions du sampleRate/s en milliseconde(Cycle time),
          celui-ci passe en rouge s'il est supérieur à 1000(Pas si la source est un fichier).  
     -Affichage du temps de traitement par seconde pris par le logiciel RTL_433 en milliseconde(Time RTL433).  

Version 1.5.4.4  17 Janvier 2022  
     - Modifications mineurs pour Honeywell_cm921.  
          -boiler_modulation_level.  
          -commande ticker.  
     - Correction crash à la fermeture des fenêtres listDevices si SDRSharp framework 6.0 à partir de la version SDRSharp 1830.  
     - Temporisation de 100ms en mode rejeu (source iq file(.wav)).Temporisation passée à 1000ms à partir de V1.5.5.0
Version 1.5.4.3  12 Janvier 2022  
     - Ajout de l'ID pour identifier les devices.->supression à partir de V1.5.5.0 problème pour TPMS laissé IDS pour Honeywell 921.  
Version 1.5.4.2  10 Janvier 2022  
     -Correctif fenêtre listMessage sur réception de message de longueur différente.  
Version 1.5.4.1  9 Janvier 2022  
     -Correctif fenêtre listMessage sur réception de message de longueur différente. ajout de 10 colonnes au lieu de 2 si -MLevel.  
     -A voir pour ajouter des colonnes en cours de réception.  
Version 1.5.4.0  6 January 2022  
     -Ajout d'un dossier dll 1.5.4.0 specifique pour SDRSharp 1784.  
Version 1.5.4.0  1 Janvier 2022
     -Mise a jour de la version RTL_433(V21.12 du 29 Décembre 2021, la précédente datait du 1 Mars 2021).  
           -Ajout de 27 devices(defaut/defaut+disabled/tous)(150/174/180)-->(177/202/208).  
     -Ajout d'une colonne nombre de message recus dans la fenetre list devices.  
Version 1.5.0.0  1 Août 2021
  - Modifications  
      -Ajout d'un troisième bouton radio sous le bouton start pour afficher un nouveau type de fenêtre:  
         Affichage d'une fenêtre par device qui contient tous les messages recus dans la limite de 10 fois le nombre de device maximum paramétré dans .config * 10.  
         100*10 par défaut.  
         Le bouton export permet d'enregistrer les données au format texte rechargeable par un tableur(calc... séparateur:tabulation).  
         Le nom du fichier est celui de la fenêtre.ATTENTION écrasement du fichier sans confirmation.  
         Si le dossier Recordings de SDRSharp existe, les fichier seront enregistrés dedans sinon dans le dossier SDRSharp.  
Version 1.5.1.0  
      -Fenêtre list message affichage du dernier message en premier.
Version 1.5.2.0  
      -Export du premier message en premier comme 1.5.0.0.  
Version 1.5.3.0  
  -Correctif  
         Fenêtre list messages  
           Erreur d'index lorsque le signal est faible(Je n'ai pas réussi à la reproduire)
           Ajout test sur le nombre de colonne a suivre...  
           Ajout de 2 colonnes supplémentaires, pour voir si le problème vient d'une mauvaise entête de colonne ce qui permettra de l'afficher.  
           ça ne devrait pas arriver, le nom des entêtes sont en "dur" dans le code RTL_433 ?  
         Changement de référence SDRsharp pour la compilation:  
           Le plugin_rtl433 avec la version sdrsharp-x86-dotnet4(1784) ou 1777 ne fonctionne qu'avec la version utilisée en référence(1632)  
           dans visual studio.  
           En référençant la version sdrsharp-x86-dotnet4(1784), le plugin fonctionne sur la 1784 mais aussi sur la 1777...  
Version 1.5.3.1  
  -Modifications  
        -Ajout d'une case à cocher dans le cadre "enabled devices disabled" pour traiter les appareils invalidés(.disabled = 1 dans les fichiers devices)  

Version 1.4.1.0  7 Juillet 2021  
  - Modifications  
      Garde la console ouverte sur stop plugin pour lire les informations verbose(version RTL_433 1.3.2.0).  
  - Corrections  
      Crash cu8 to .wav.  
  - Modification sur le paramétrage de TUNER AGC et RTL AGC  
      Tuner AGC:on(corresponds à gain auto with rtl433) peux être mis sur off.\n")  
      RTL AGC:on.(pas AGC panel) peux être mis OFF pour des bons niveaux de reception .\n")    
Version 1.4.0.0  Juillet 2021  
  - Evolutions  
        Ajout de 2 boutons radio sous le bouton start.  
           - Graph:ouverture d'une fenêtre par device(identique aux versions précédentes).  
           - List devices:Affichage d'une seule liste contenant une ligne par device avec la dernière donnée reçue(demande d'un internaute).  
                - Les colonnes sont ajoutées à la réception de nouveaux libellés de donnée.  
                - A la fermeture de cette fenêtre, il est donné la possibilité d'enregistrer les données  
                  au format texte rechargeable par un tableur(calc.. séparateur:tabulation).  
                  Le fichier se nomme devices.txt, ATTENTION écrasement du fichier sans confirmation.  
                - A la sélection de ce type de fenêtre(bouton List devices), il est possible de recharger le fichier.  
                  Cette fenêtre est limitée à 100 colonnes et à 10 fois le nombre de device maximum paramétré dans .config * 10.  
                  100*10 par défaut.  
                Il est possible de passer d'un mode à l'autre sans arrêter le plugin.  
       - Ajout d'un bouton radio au dessus de la liste des devices.  
           - Hide select:pas de traitement des devices sélectionnés(identique aux versions précédentes).  
           - Show select:traitement que des devices sélectionnés.  
  - Corrections  
        -  Plantage sur un deuxième appel de .cu8 to .wav.  
        -  Correction des temps sur les graphiques qui ne prenaient pas en compte le sample rate.  
Version 1.3.1.0  8 Juin 2021  
  - Modifications  Les sample rate > 250000 sont gérés, 250000 reste quand même préférable.  
        L'enregistrement au niveau des fenêtres devices n'est autorisé qu'à 250000.  
        Cette modification était une demande d'un internaute pour pouvoir utiliser Airspy mini qui ne descend pas à 250000.  
  - Corrections  
         Avec -vvv, une fenêtre "zombi" pouvait s'afficher due aux messages reçu.  
Version 1.3.0.0  Juin 2021  
  - Evolutions  
    - Chargement de la liste des devices et du forçage de la fréquence SDRSharp au premier start du plugin et pas au chargement.  
    - Ajout de messageBox si problème au chargement.  
    - Mémorisation de l'option Metadata.  
    - Mémorisation de l'option Fréquence.  
  - Modifications  
        - Pour limiter la consommation de mémoire, les graphiques sont affichés sur les 5 premiers et à la demande pour les autres
        (bouton Display curves sur les fenêtres.) cette limite est mémorisée dans le fichier SDRSharp.config ou SDRSharp.exe.config 
        selon les versions après un premier chargement correct du plugin. Clé RTL_433_plugin.nbDevicesWithGraph.  
        - Limitation du maximum de fenêtre devices à 100, cette limite est mémorisée dans le fichier SDRSharp.config
        ou SDRSharp.exe.config selon les versions après un premier chargement correct du plugin. Clé RTL_433_plugin.MaxDevicesWindows
        (cette valeur peut être baissée si des postes ont encore des problèmes de mémoire sans les graphiques).  
        - Les options RTL_433 sont affichées sur la fenêtre message du plugin.  
  - Corrections  
        - cu8 to wav.  -->  rappel:les fichiers cu8(-S) et MONO et STEREO (cu8 to wav) se situent au niveau de l'exe SDRSharp.
                            Les fichier MONO et STEREO générés à partir de la fenêtre device se situent dans Recordings s'il existe sinon 
                            au niveau de l'exe SDRSharp.
        - record MONO.  
  - Informations  
       - Si vous avez utilisés une version précédente du plugin, vous pouvez supprimer les 3 clés dans le fichier 
            SDRSharp.config ou SDRSharp.exe.config :DataConvNative DataConvSI et DataConvNativeCustomary que j'ai remplacé par une seul ligne 
            RTL_433_plugin.DataConv.  
       - Si la source SDRSharp est sur RTL SDR-TCP, un paramétrage qui fonctionne:  
            - Open SDRSharp  
            - Sélectionner la source RTL-SDR TCP.  
            - Configure source:  
              - Host  
              - Port  
              - Sample rate=0.25MSPS  
              - Sélectionner RTL AGC  
             - Démarrer SDRSharp  
               - Sélectionner Tuner AGC  
             - Démarrer le plugin.  
  - Tests  
        - Je test en développement sur la version SDRSharp Github 1632 .  
        - Je valide sur la dernière version SDRSharp (1811) sous Windows 10 64bits.  
  
Version 1.11  Mai 2021  
  - Evolutions  
    - Ajout de l'option -C data conv.  
    - La fenêtre device affiche les 4 derniers messages.  
      J'ai ajouté ces 2 options pour les messages TPMS pour:  
        - afficher la pression en kilo-pascal et des degrés celcius(option SI).  
        - et pour avoir les pressions des 4 pneus.  
    - Ajout des courbes pulse data en FSK.  
  - Modifications  
    - Reprise de la gestion console pour cohabitation avec d'autres plugin avec console(DSDPlus...)  
  - Corrections  
    - Correctif pour pouvoir rejouer les fichiers générés par one shoot record(les 50000 échantillons pour diminuer
      le temps de cycle ne suffisaient pas (enregistrement des 5 derniers buffer de 50000).  
  - Informations  
    - Le nombre de fenêtre device n'est pas limité par le code, la limitation est liée à la mémoire
	  (ordre de grandeur pour 130 fenêtres : 1G de mémoire)  
    - Le plugin fonctionne avec AirSpy R2(https://www.rtl-sdr.com/rtl433-plugin-for-sdr-now-available/#comments).  

Version 1.10  Avril 2021  
    - Ajout des graphiques Pulses data, analyse et fm.  
        nota:les graphiques analyse et fm sont synchrones(même période d'échantillonnage).  
            les 3 graphiques sont synchronisés dans le temps par rapport au premier échantillon.  
    - Ajout de l'option save -S. 
           Cette version enregistre les fichiers .cu8 dans le dossier de SDRSharp.exe.	
    - Ajout de la liste des devices pour l'option -R  
      -Selection des devices à ne pas afficher.  
    - Ajout du fichier de test dans install\Recordings for rejeu par SDRSharp(Baseband Files(*))  

Version 1.00  Mars 2021

# Configuration de test  
- Système d'exploitation :Windows 10.  
- Clé DVB-T+FM+DAB 820T2 & SDR DV3 USB2.0  
- Version modifiée de rtl_433 du 1 Mars 2021.  

## Programmation  
2 possibilités:
    -1. SDRSharp_light.sln: Ce projet permet de générer les 3 dll citées pour l'installation ci-dessus.  
       Il faudra auparavant télécharger:  
       - Le projet GraphLib: https://github.com/marco402/GraphLibSpecific  
       - Le projet Rtl_433_dll: https://github.com/marco402/Rtl_433_dll-for-plugin-SdrSharp  
       - SDRSharp (testé avec la version 1.0.0.1788).  
       Mettre à jour si nécessaire les chemin de chargement, de compilation et des références.  
    -2. SDRSharp.sln: En plus de la version light, télécharger le projet d'origine SDRSharp sur Github: https://github.com/SDRSharpR/SDRSharp  
 
​

