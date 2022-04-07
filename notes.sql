create table schedules(Id TEXT, JobId INTEGER, cron TEXT);
create table schedule_params(ScheduleId TEXT, ParamName TEXT, Value TEXT);
create table runs(Id TEXT, JobId INTEGER, state TEXT);


insert into schedules values ("881a389b-6cc0-4094-86d2-9473fd55d01d", 1, "*/5 * * * * ?");
insert into schedules values ("374f4a05-b986-4637-97b9-f6703dda5317", 2, "*/8 * * * * ?");
insert into schedules values ("aaaaaaaa-b986-4637-97b9-f6703dda5317", 2, "*/3 * * * * ?");

insert into schedule_params values ("374f4a05-b986-4637-97b9-f6703dda5317", "Connector Id", "8");
insert into schedule_params values ("aaaaaaaa-b986-4637-97b9-f6703dda5317", "Connector Id", "3");