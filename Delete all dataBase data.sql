delete from Bookings
delete from Appointments
delete from AppointmentTimes
delete from Doctors 
delete from AspNetUsers

DBCC CheckIdent(Bookings, RESEED,0)