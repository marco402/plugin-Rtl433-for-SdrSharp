
French version after English version.  
    You can display dump windows on https://marco40github.wixsite.com/website/plugin-sdrsharp-pour-rtl-433?lang=en  
Version Francaise après la version Anglaise.  
    Vous pouvez visualiser des dump fenêtres sur https://marco40github.wixsite.com/website/plugin-sdrsharp-pour-rtl-433  


Version 1.10  
    -Added  pulses data, analysis and fm graphics.  
        note: analysis and fm are synchronous(same sampling period).  
            The 3 graphs are time synchronized with the first sample.  
    -Added save -S option.  
    -Added list devices for option -S.  
         -Select no display devices.  
    -Add file test in install\Recordings for replay with input SDRSharp(Baseband Files(*))  

## Use  
Version 1.00  
# Display  
    -Display of each received device in a different window.  
    -Verbose output display in the console window.  
    -Frequency spectrum display in replay mode.  
# Record  
-Record data for each device in . wav file  
    - either in stereo mode for I and Q which makes it possible to replay this file by selecting it by the SDRSharp(Baseband Files(*.wav) source).
To change the file, configure SDRSharp button.  
    - either in mono mode for the IQ module which allows the signal to be displayed with Audacity or other third-party software.  
    To start these recordings, select MONO STEREO at rtl433 interface, both are possible at the same time.  
    Then launch "record one shoot" at the window of the device . 1 is only selectable, it is the last selected that is taken into account.  
For these recordings, it is preferable to center the frequency on the device selected to have a frequency spectrum centered.  
    If the Recordings folder of SDRSharp exists, the files will be saved in it otherwise in the SDRSharp folder.  
    These files contain all the data entered at once(50,000 bytes) and are not limited to the device data.  
    File name are:  
        -Device name.  
        -Device ID.  
        -Channel if exist.  
        -Frequency.  
        -Sample.  
        -Date.  
        -Heure.  
        -MONO or STEREO  
# Setting up sdrSharp  
The plugin processes the raw IQ data, so a priori, only the parameters located in the upper band changes its operation.  
Configure source button:  
    -Sampling mode:quadrature sampling  
    -Sample Rate:0.25 MSPS (value of rtl433)  
    -RTL AGC:on.(not the AGC panel)  
    -Tuner AGC:on.  
    -So is the frequency.   

# File Conversion  
    -Convert .cu8 to .wav STEREO files to reload with SDRSharp.  
    -The file names are completed by STEREO or MONO, cu8 is replaced by wav.  
    -The sampling rate is taken at the end of the file name between _ and k.  
    -If it does not exist, 250,000 is taken by default.  

# Delta with RTL_433  
    The size of the receive memory is 50000 bytes compared to 262144 for rtl_433 this size works well but 50000 allows  
	to shorten the processing time of a cycle.  
​
# The options implemented in this first version:  
    -The different verbose (to the console).  
    -Metadata  
        -Label(the fields are added to the device windows).  
# Frequency  
    It is possible to select the different frequencies quoted on the site  
	https://triq.org/rtl_433/#building-installation  
The free selection allows to launch the plugin without changing the frequency.  
​
# Configuration  
    -Operating systeme:Windows 10.  
    -Clé DVB-T+FM+DAB 820T2 & SDR DV3 USB2.0  
    -Modified version of rtl_433 of March 1, 2021.  


## Installation  
​
-SDRSharp installation (tested with version 1.0.0.1788)  
    -Place the files in the install folder (SDRSharp.Rtl_433.dll rtl_433.dll and GraphLib.dll) in the SDRSharp folder.  
    -Add the line add key="RTL_433" value="SDRSharp.Rtl_433.Rtl_433_Plugin, SDRSharp . Rtl_433" /> in the plugins.xml file   

## Programming  
2 possibilities:  
    -1-SDRSharp_light.sln: This project generates the 3 dll mentioned for the above installation.  
      You will need to download:  
        -GraphLib Project: https://github.com/marco402/GraphLibSpecific  
        -Rtl_433_dll: https://github.com/marco402/Rtl_433_dll-for-plugin-SdrSharp
        -SDRSharp (tested with version 1.0.0.1788).  
      Update the load and build paths if necessary.  
    -2-SDRSharp.sln: In addition to the light version, download the original SDRSharp project on Github: https://github.com/SDRSharpR/SDRSharp  


## Utilisation  
Version 1.10  
    -Ajout des graphiques Pulses data, analyse et fm.  
        nota:les graphiques analyse et fm sont synchrones(même période d'échantillonnage).  
            les 3 graphiques sont synchronisés dans le temps par rapport au premier échantillon.  
    -Ajout de l'option save -S.  
    -Ajout du fichier de test dans install\Recordings for rejeu par SDRSharp(Baseband Files(*))  
	
Version 1.00  

# Affichage  
    -Affichage de chaque appareil reçu dans une fenêtre différente.  
    -Affichage des sorties verbose dans la fenêtre console.  
    -Affichage du spectre de fréquence en mode rejeu.  
# Enregistrement  
-Enregistrement  des données pour chaque appareil en fichier .wav  
        - soit en mode stéréo pour I et Q ce qui permet de rejouer ce fichier en le sélectionnant par la source SDRSharp(Baseband Files(*)).  
    Pour changer de fichier, bouton configure de SDRSharp.  
        - soit en mode mono pour le module de IQ ce qui permet d'afficher le signal avec un logiciel tiers Audacity ou autre.  
    Pour lancer ces enregistrements, sélectionner MONO STEREO au niveau de l'interface rtl433 , les 2 sont possible en même temps.  
    Puis lancer "record one shoot" au niveau de la fenêtre de l'appareil .1 seul est sélectionnable, c'est le dernier sélectionné qui est pris en compte.  
    Pour ces enregistrements, il est préférable de centrer la fréquence sur l' appareil sélectionne pour avoir un spectre des fréquences centré.  
    Si le dossier Recordings de SDRSharp existe, les fichier seront enregistrés dedans sinon dans le dossier SDRSharp.  
    Ces fichier contiennent toutes les données saisies d'un coup(50000 octets) et ne sont pas limitées au données de l'appareil.  
    Les noms des fichiers comprennent:  
        -Le nom de l'appareil.  
        -ID de l'appareil.  
        -Le canal si présent.  
        -La fréquence.  
        -Le nombre d'échantillon par seconde.  
        -Date.  
        -Heure.  
        -MONO ou STEREO  

# Paramétrage de sdrSharp  
Le plugin traite les données brutes IQ donc à priori, seul les paramètres situés dans le bandeau supérieur change son fonctionnement.  

# Bouton configure source:  
    -Sampling mode:quadrature sampling  
    -Sample Rate:0.25 MSPS (valeur de rtl433)  
    -RTL AGC:on.(pas le panel AGC)  
    -Tuner AGC:on.  
    -Ainsi que la fréquence.  

# Conversion de fichier  
    -Conversion de fichier .cu8 vers des fichiers .wav STEREO pour les recharger avec SDRSharp.  
    -Les noms de fichiers sont complétés par STEREO ou MONO, cu8 est remplacé par wav.  
    -La vitesse d'échantillonnage est prélevée à la fin du nom du fichier entre _ et k.  
    -Si celle-ci n'existe pas, 250000 est prise par défaut.  

# Delta avec RTL_433  
    La taille de la mémoire de réception est de 50000 octets contre 262144 pour rtl_433 cette taille fonctionne bien  
	mais 50000 permet de raccourcir le temps de traitement d'un cycle.  
​
# Les options  implémentées dans cette première version:  
    -Les différents verbose(vers la console).  
    -Metadata  
        -Label(les champs s'ajoutent aux fenêtres des appareils).  

# Fréquences  
    Il est possible de sélectionner les différentes fréquences citées sur le site  
	https://triq.org/rtl_433/#building-installation  
La sélection free permet de lancer le plugin sans changer la fréquence.  
​
# Configuration  
-Système d'exploitation :Windows 10.  
-Clé DVB-T+FM+DAB 820T2 & SDR DV3 USB2.0  
-Version modifiée de rtl_433 du 1 Mars 2021.  

## Installation  
​
-Installation de SDRSharp(testé avec la version 1.0.0.1788)  
    -Placer les fichiers du dossier install (SDRSharp.Rtl_433.dll rtl_433.dll et GraphLib.dll) dans le dossier SDRSharp.  
    -Ajouter la ligne <add key="RTL_433" value="SDRSharp.Rtl_433.Rtl_433_Plugin, SDRSharp .Rtl_433" /> dans le fichier plugins.xml  

## Programmation  
2 possibilités:
    -1-SDRSharp_light.sln: Ce projet permet de générer les 3 dll citées pour l'installation ci-dessus.  
       Il faudra auparavant télécharger:  
        -Le projet GraphLib: https://github.com/marco402/GraphLibSpecific  
        -Le projet Rtl_433_dll: https://github.com/marco402/Rtl_433_dll-for-plugin-SdrSharp  
        -SDRSharp (testé avec la version 1.0.0.1788).  
       Mettre à jour si nécessaire les chemin de chargement et de compilation.  
    -2-SDRSharp.sln: En plus de la version light, télécharger le projet d'origine SDRSharp sur Github: https://github.com/SDRSharpR/SDRSharp  
 
​

