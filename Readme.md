![Build succeeded][build-shield]
![Test passing][test-shield]
[![Issues][issues-shield]][issues-url]
[![Issues][closed-shield]][issues-url]
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![License][license-shield]][license-url]

# Intelligent house App
#### H5 App 3 group project
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>

- [Case](#case)
- [Requirements](#requirements)
- [Architecture diagram](#architecture-diagram)
- [Roadmap](#roadmap)
- [Summary and rundown](#summary-and-rundown)
- [Getting started](#getting-started)
- [MQTT Topics](#mqtt-topics)
- [Libraries](#libraries)
- [Components](#components)
- [License](#license)
- [Contact](#contact)
</details>

# Case
Build master and slave units for the *Intelligent house* - the master unit must be the "heart" of the communication, and control several functions around the house via the slave units.
* Perimeter alarm system
* Internal alarm system
* Climate control
* Gas detection
* Entry system
* User-friendly interaction
* General smart-home functions (lights, doors, etc.)
<p align="right">(<a href="#top">back to top</a>)</p>

# Requirements
- [x] Kan vise aktuel (seneste måling) af temperatur og humidity og evt. andre informationer.
- [x] Kan vise en graf over målingerne, hvor man kan vælge mellem seneste time, dag og uge.
- [x] Der skal være en knap, der via MQTT kan aktivere en servo (og simulere at man åbner et vindue eller tænder for ventilationen).
- [x] App'en skal opbygges med MVVM design pattern og Dependency Injection.
- [x] Kan vise seneste data, hvis nettet afbrydes. 
- [X] Er robust overfor ustabil netforbindelse.
- [x] Projektet afleveres i Github med en god Readme-fil og præsenteres for klassen. Readme-filen markerer også hvilke mål, der er nået.

## Optionelle krav

- [x] Mulighed for at kunne vælge forskellige målesessions, f.eks. svarende til forskellige rum i huset.
- [x] Man kan også ønske sig en alarm, der adviserer om at temperaturen er kommet udenfor en given grænse.
<p align="right">(<a href="#top">back to top</a>)</p>

# Architecture diagram
![architecture diagram](/Docs/Architecture_Diagram.drawio.png)



#  Summary and rundown
The **IntelliHouse2000App** is a all-in-one microcontroller combo that provides climate control, monitoring and alarm/entry functions for the entire house. You can have sensors in all rooms, and set the parameters for each sensor.
If an event is triggered while the alarm is armed, no apparent function will happen in the house, but the log will be updated and the user will get a message<sup>1</sup> with the event, timestamp and what sensor triggered it. If however the alarm is disarmed (that is, the user is home) **IntelliHouse2000** will take action on the event.
> If you forget to turn off your car in the garage, and the sensor detects rising CO<sub>2</sub> levels, the user will be warned, displays around the house will show the event, and the garage door will open incrementally until the sensor value returns to normal

> If the humidity in the house rises rapidly, the appropriate window will be opened incrementally until the sensor detects a drop in humidity. As an extra function<sup>2</sup> you can add weather sensor as well, so the window *doesn't* open if the humidity outside is higher than inside, or it rains.

> No matter what action have been taken (open doors, windows etc.) those will automatically close when the alarm system is armed. This happens with both perimeter and full arm.
<p align="right">(<a href="#top">back to top</a>)</p>


# Getting started
To add a device go to the appropriate section (e.g. Climate) and locate the header-file (ends with .h) - there you can see the pin-defines, where all you have to do it add your new device to that list like this: `#define NewSensor 8` where *NewSensor* is the "friendly name" of your sensor, and *8* is the pin-number you're using on the board.
Next thing is to "start" the sensor `DHT newDHT(NewSensor, Type)`where *newDHT* is the name of the object and *Type* is the type of sensor (e.g. DHT11).

Then all you do is to add the check to the source code
```
temperature2 = newDHT.readTemperature();
humidity2 = newDHT.readHumidity();
```

Make sure to read the setup for each device, some communicate by one-wire (like the DHT11), some use SPI *(Serial Peripheral Interface)*, some use I<sup>2</sup>C *(Called **Wire** in Arduino)* and others use analog input.

<p align="right">(<a href="#top">back to top</a>)</p>



# MQTT Topics
| Topics                               | Access   | Method  |
| :----------------------------------- | :------- | :------ |
| home/alarm/arm                       | External | Pub/Sub |
| home/alarm/alarm                     | External | Sub     |
| home/alarm/alarm                     | Internal | Pub     |
| home/climate/status/#                | External | Sub     |
| home/climate/status/[section]/[type] | Internal | Pub     |
| home/climate/servo                   | External | Pub     |
| home/log/[logLevel]/[type]           | Internal | Pub     |
| home/log/#                           | External | Sub     |

# Libraries


<p align="right">(<a href="#top">back to top</a>)</p>



# License
* Hardware: CC-BY-LA (Creative Commons)
* API: GPLv3
* Frontend: GPLv3
* Mobile: GPLv3
<p align="right">(<a href="#top">back to top</a>)</p>

# Contact
- Peter Hymøller - peterhym21@gmail.com
  - [Twitter](https://twitter.com/peter_hym)
- Nicolai Heuck - nicolaiheuck@gmail.com
- Jan Andreasen - jan@tved.it
  - [![Twitter][twitter-shield]][twitter-url]

Project Link: [https://github.com/Thoroughbreed/H5_Embedded_Project](https://github.com/Thoroughbreed/H5_Embedded_Project)
<p align="right">(<a href="#top">back to top</a>)</p>

<sup>1</sup> - Informs the user via mobile app over the MQTT protocol

<sup>2</sup> - Function not built in yet

<sup>3</sup> - Logs via MQTT to a database in the following layers: Debug, Info, Critical.


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[build-shield]: https://img.shields.io/badge/Build-passed-brightgreen.svg
[test-shield]: https://img.shields.io/badge/Tests-passed-brightgreen.svg
[contributors-shield]: https://img.shields.io/github/contributors/Thoroughbreed/H5_Embedded_Project.svg?style=badge
[contributors-url]: https://github.com/Thoroughbreed/H5_Embedded_Project/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Thoroughbreed/H5_Embedded_Project.svg?style=badge
[forks-url]: https://github.com/Thoroughbreed/H5_Embedded_Project/network/members
[issues-shield]: https://img.shields.io/github/issues/Thoroughbreed/H5_Embedded_Project.svg?style=badge
[closed-shield]: https://img.shields.io/github/issues-closed/Thoroughbreed/H5_Embedded_Project?label=%20
[issues-url]: https://github.com/Thoroughbreed/H5_Embedded_Project/issues
[license-shield]: https://img.shields.io/github/license/Thoroughbreed/H5_Embedded_Project.svg?style=badge
[license-url]: https://github.com/Thoroughbreed/H5_Embedded_Project/blob/master/LICENSE
[twitter-shield]: https://img.shields.io/twitter/follow/andreasen_jan?style=social
[twitter-url]: https://twitter.com/andreasen_jan
[twitter-shield-ptr]: https://img.shields.io/twitter/follow/peter_hym?style=social
[twitter-url-ptr]: https://twitter.com/peter_hym
