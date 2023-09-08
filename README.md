# RailFlow
A web application for managing railways. 

The application will be designed to provide dedicated panels for non-logged-in users, logged-in users, employees and managers.

The panel for a non-logged-in user allows searching for railroad connections. To search for a preferred connection, it is necessary to select a starting station, an ending station and a travel date selected in the calendar. The application also searches for connections that require a transfer. After selecting a specific trip, the user has the opportunity to review the route of the trip and stops at stations.

The passenger panel of the logged-in user gives the opportunity to purchase a ticket on the train. The application also generates a ticket in the form of a PDF file, which the user can download. The logged-in user can also review the history of his/her own trips.

The employee panel is extended by the possibility to review the schedule of all trips to which the employee will be assigned in the current month. 

The manager panel allows you to set schedules for employees. It also allows you to modify the schedule and routes for a given line. The manager panel also allows you to delete accounts from the system.

The schedule is generated automatically one month ahead.

The application will be developed in client-server architecture using a database. 
ASP.NET technology will be used to create the server layer. The client side will be created using React library in Javascript.
