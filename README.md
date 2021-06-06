# Placement Management System

The project acts as a utility for the placement officers of any institution. The current system requires people to manually enter professional affiliations into some Excel sheet and maintain it as the institution moves ahead. Our project provides 2 interfaces; one for admin and one for students, and a central database which makes the process much easier for both parties involved.

Implemented using MS Visual Studio Community 2015 and MS SQL Server using Entity Framework.

  1. Admin Module:
    This module has the primary role in the system as the admin is able to add/review/sort/search/delete student records.
  2. Student Module:
    This module helps the students to review their information and put in a request to edit their details (if any) to the Admin.
    
Both the above modules will have the functionality of sign in, sign out. The accounts for admin will be created when the application is deployed. The admins can then create accounts for the students and let them know their credentials.

# How to Run?
Download the project. Change "ConnectionStrings" in Web.config and run Code First Migrations to create the database and run the application. You can find admin credentials in the Roles modules.
