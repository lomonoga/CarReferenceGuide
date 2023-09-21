CREATE ROLE car_user;
ALTER
USER car_user WITH PASSWORD 'car_user';
CREATE
DATABASE car_reference_guide;
GRANT ALL PRIVILEGES ON DATABASE
car_reference_guide TO car_user;