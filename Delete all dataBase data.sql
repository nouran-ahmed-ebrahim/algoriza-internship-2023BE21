delete from Bookings
delete from Appointments
delete from AppointmentTimes
delete from Doctors 
delete from AspNetUsers

DBCC CheckIdent(Bookings, RESEED,0)DBCC CheckIdent(Appointments, RESEED,0)DBCC CheckIdent(AppointmentTimes, RESEED,0)DBCC CheckIdent(Doctors, RESEED,0)
