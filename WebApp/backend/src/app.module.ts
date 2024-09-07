import { Module } from '@nestjs/common';
import { SequelizeModule } from '@nestjs/sequelize';
import { ConfigModule } from "@nestjs/config";
import { Car } from './regulation_objects/car/car.model';
import { Sign } from './regulation_objects/sign/sign.model';
import { TrafficLight } from './regulation_objects/traffic_light/traffic_light.model';
import { TicketCar, TicketSign, TicketTrafficLight, Ticket } from './regulation_objects/ticket';
import { CarModule } from './regulation_objects/car/car.module';
import { SignModule } from './regulation_objects/sign/sign.module';
import { TrafficLightModule } from './regulation_objects/traffic_light/traffic_light.module';
import { TicketModule } from './regulation_objects/ticket/ticket.module';

@Module({
  imports: [
    ConfigModule.forRoot({
      isGlobal: true, // Делаем доступным во всех модулях
      envFilePath: '.env', // Указываем путь к .env файлу
    }),
    SequelizeModule.forRoot({
      dialect: 'postgres',
      host: String(process.env.POSTGRES_HOST),
      port: Number(process.env.POSTGRES_PORT),
      username: String(process.env.POSTGRES_USER),
      password: String(process.env.POSTGRES_PASSWORD),
      database: String(process.env.POSTGRES_DB),
      models: [
        Car,
        Sign,
        TrafficLight,
        Ticket,
        TicketCar, // Добавьте модель сюда
        TicketSign,
        TicketTrafficLight,
      ],
      autoLoadModels: true,
      synchronize: true, // Set to false in production!
    }),
    CarModule,
    SignModule,
    TrafficLightModule,
    TicketModule,
  ],
  // controllers: [AppController],
  // providers: [AppService],
})

export class AppModule {}