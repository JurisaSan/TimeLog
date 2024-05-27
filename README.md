# TimeLog
This repository contains the code for a time logging application.

![image](https://github.com/JurisaSan/TimeLog/assets/164797445/3d955523-acf8-4255-b2ad-ff44974cbce7)

## Features

- Log time entries with start and end times
- Calculate total time logged
- View and edit time entries
- Generate reports
- Being started, the application will load the last saved tasks from log file.

This application is designed to work seamlessly with Microsoft CRM, 
offering a more flexible approach to time logging. 
The optimal way to utilize this application involves recording the time spent on each task, 
followed by manual entry of this data into the CRM system. 
The application’s most significant advantage becomes apparent when managing four or more tasks. 
It allows for swift switching between task timers, eliminating the need to open the CRM system repeatedly, 
thus saving valuable time.

## Installation

1. Clone the repository: `git clone https://github.com/JurisaSan/TimeLog.git`

## Usage

Update the App.config file with the desired directory for TimeLog.txt, which contains the history of time entries.
Build the project in Visual Studio 2022 and run the application.
The application always contains one task entry. 
To add a new task, click the "Add Task" button and enter the task name.
To log time, select silver button to start the timer and select another task's silver button or StandBy button to stop the timer.
To delete a task, empty the last task name and click the red "-" button. 
Please note that only the last task can be deleted. 
Delete button will be enabled only when task description is empty.  

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. 
