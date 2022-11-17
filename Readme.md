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
	- [Optionel Requirements](##Optionel-Requirements)
- [Architecture diagram](#architecture-diagram)
- [Summary and rundown](#summary-and-rundown)
- [Folder Structure](#Folder-Structure)
- [API Calls](#API-Calls)
- [MessageCenter](#MessageCenter)
- [MQTT Topics](#mqtt-topics)
- [Libraries](#libraries)
- [App Pages](#App-Pages)
- [License](#license)
- [Contact](#contact)
</details>


# Case
Build a APP for the [Intelligent House](https://github.com/Thoroughbreed/H5_Embedded_Project)

- Home page for alarm
- Page for log with 3 tabs for 3 log-levels
- Page for climate with 3 tabs for 3 rooms


<p align="right">(<a href="#top">back to top</a>)</p>


# Requirements
- [x] Kan vise aktuel (seneste måling) af temperatur og humidity og evt. andre informationer.
- [x] Kan vise en graf over målingerne, hvor man kan vælge mellem seneste time, dag og uge.
- [x] Der skal være en knap, der via MQTT kan aktivere en servo (og simulere at man åbner et vindue eller tænder for ventilationen).
- [x] App'en skal opbygges med MVVM design pattern og Dependency Injection.
- [x] Kan vise seneste data, hvis nettet afbrydes. 
- [X] Er robust overfor ustabil netforbindelse.
- [x] Projektet afleveres i Github med en god Readme-fil og præsenteres for klassen. Readme-filen markerer også hvilke mål, der er nået.

## Optionel Requirements
- [x] Mulighed for at kunne vælge forskellige målesessions, f.eks. svarende til forskellige rum i huset.
- [x] Man kan også ønske sig en alarm, der adviserer om at temperaturen er kommet udenfor en given grænse.

<p align="right">(<a href="#top">back to top</a>)</p>


# Architecture diagram
![architecture diagram](/Docs/Architecture_Diagram.drawio.png)

<p align="right">(<a href="#top">back to top</a>)</p>


#  Summary and rundown
The **IntelliHouse2000** is a all-in-one microcontroller combo that provides climate control, monitoring and alarm/entry functions for the entire house. You can have sensors in all rooms, and set the parameters for each sensor.
If an event is triggered while the alarm is armed, no apparent function will happen in the house, but the log will be updated and the user will get a message<sup>1</sup> with the event, timestamp and what sensor triggered it. 
If however the alarm is disarmed (that is, the user is home) **IntelliHouse2000** will take action on the event.
All of this is then displayed on the App for the **IntelliHouse2000**, **IntelliHouse2000App** with a easy to use UI for userfrendlynes.
> If you forget to turn off your car in the garage, and the sensor detects rising CO<sub>2</sub> levels, the user will be warned, displays around the house will show the event, and the garage door will open incrementally until the sensor value returns to normal

> If the humidity in the house rises rapidly, the appropriate window will be opened incrementally until the sensor detects a drop in humidity. As an extra function<sup>2</sup> you can add weather sensor as well, so the window *doesn't* open if the humidity outside is higher than inside, or it rains.

> No matter what action have been taken (open doors, windows etc.) those will automatically close when the alarm system is armed. This happens with both perimeter and full arm.

<p align="right">(<a href="#top">back to top</a>)</p>


# Folder Structure

![Folder structure](/Docs/FolderStructure.png)

We use MVVM, Services, Repositories and Helpers 

And uses FBF (Folder By Feature)


<p align="right">(<a href="#top">back to top</a>)</p>


# API Endpoints
**OPS:** Full api documentaiton can be found on [swagger](https://mqtt-api.tved.it/swagger/index.html)

Base URL : https://mqtt-api.tved.it/

| Method | Uri           | Description                                                            | Parameters     | Parameter formats                    |
| :----- | :------------ | :--------------------------------------------------------------------- | :------------- | :----------------------------------- |
| GET    | /             | Smoke test                                                             |                |                                      |
| GET    | /all          | Get last 50 logs                                                       |                |                                      |
| GET    | /info         | Get last 50 logs with log level info                                   |                |                                      |
| GET    | /debug        | Get last 50 logs with log level debug                                  |                |                                      |
| GET    | /system       | Get last 50 logs with log level system                                 |                |                                      |
| GET    | /critical     | Get last 50 logs with log level critical                               |                |                                      |
| GET    | /kitchen      | Get all temperature and humidity readings from the kitchen since ts    | ts (timestamp) | Datetime format: yyyy-MM-dd HH:mm:ss |
| GET    | /kitchen/1    | Get last temperature and humidity reading from the kitchen             |                |                                      |
| GET    | /bedroom      | Get all temperature and humidity readings from the bedroom since ts    | ts (timestamp) | Datetime format: yyyy-MM-dd HH:mm:ss |
| GET    | /bedroom/1    | Get last temperature and humidity reading from the bedroom             |                |                                      |
| GET    | /livingroom   | Get all temperature and humidity readings from the livingroom since ts | ts (timestamp) | Datetime format: yyyy-MM-dd HH:mm:ss |
| GET    | /livingroom/1 | Get last temperature and humidity reading from the livingroom          |                |                                      |
| GET    | /airq         | Get all air quality readings since ts                                  | ts (timestamp) | Datetime format: yyyy-MM-dd HH:mm:ss |
| GET    | /airq/1       | Get last air quality reading                                           |                |                                      |



# MessageCenter
| Topics								| Method	|
| :------------------------------------ | :-------- |
| AlarmArmedSubject						| Pub/Sub	|
| AlarmPartiallyArmedSubject			| Pub/Sub	|
| AlarmFullyArmedSubject				| Pub/Sub	|
| Set-Humid								| Pub/Sub	|
| Set-Temp								| Pub/Sub	|

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

<p align="right">(<a href="#top">back to top</a>)</p>


# Libraries
| Name                                              | Version           |
| :------------------------------------------------ | :---------------- |
| CommunityToolkit.Mvvm                             | 8.0.0             |
| Microsoft.Maui.Dependencies                       | 6.0.547           |
| Microsoft.Maui.Extensions                         | 6.0.547           |
| Microsoft.Windows.SDK.BuildTools                  | 10.0.22000.194    |
| MQTTnet                                           | 3.1.1             |
| MQTTnet.Extensions.ManagedClient                  | 3.1.1             |
| Polly                                             | 7.2.3             |
| sqlite-net-pcl                                    | 1.8.116           |
| Syncfusion.Maui.Charts                            | 20.3.56           |
| System.Runtime.InteropServices.NFloat.Internal    | 6.0.1             |

<p align="right">(<a href="#top">back to top</a>)</p>


# App Pages

<img src="/Docs/HomePageApp.png" width="50%"/>

<img src="/Docs/LogPageApp.png" width="50%"/>

<img src="/Docs/ClimatePageApp.png" width="50%"/>




# License
* Hardware: CC-BY-LA (Creative Commons)
* API: GPLv3
* Frontend: GPLv3
* Mobile: GPLv3

<p align="right">(<a href="#top">back to top</a>)</p>


# Contact
- Peter Hymøller - peterhym21@gmail.com
  - [![Twitter][twitter-shield-ptr]][twitter-url-ptr]
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
[contributors-shield]: https://img.shields.io/github/contributors/peterhym21/H5.IntelliHouse2000App.svg?style=badge
[contributors-url]: https://github.com/peterhym21/H5.IntelliHouse2000App/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/peterhym21/H5.IntelliHouse2000App.svg?style=badge
[forks-url]: https://github.com/peterhym21/H5.IntelliHouse2000App/network/members
[issues-shield]: https://img.shields.io/github/issues/peterhym21/H5.IntelliHouse2000App.svg?style=badge
[closed-shield]: https://img.shields.io/github/issues-closed/peterhym21/H5.IntelliHouse2000App?label=%20
[issues-url]: https://github.com/peterhym21/H5.IntelliHouse2000App/issues
[license-shield]: https://img.shields.io/github/license/peterhym21/H5.IntelliHouse2000App.svg?style=badge
[license-url]: https://github.com/peterhym21/H5.IntelliHouse2000App/blob/master/LICENSE
[twitter-shield]: https://img.shields.io/twitter/follow/andreasen_jan?style=social
[twitter-url]: https://twitter.com/andreasen_jan
[twitter-shield-ptr]: https://img.shields.io/twitter/follow/peter_hym?style=social
[twitter-url-ptr]: https://twitter.com/peter_hym
