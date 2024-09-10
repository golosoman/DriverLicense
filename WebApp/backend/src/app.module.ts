import { Module } from '@nestjs/common';
import { SequelizeModule } from '@nestjs/sequelize';
import { ConfigModule } from "@nestjs/config";
import { RoadUser } from './regulation_objects/car/roadUsers.model';
import { Sign } from './regulation_objects/sign/sign.model';
import { TrafficLight } from './regulation_objects/traffic_light/traffic_light.model';
import { TicketRoadUser, TicketSign, TicketTrafficLight, Ticket } from './regulation_objects/ticket';
import { CarModule } from './regulation_objects/car/roadUsers.module';
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
        RoadUser,
        Sign,
        TrafficLight,
        Ticket,
        TicketRoadUser,
        TicketSign,
        TicketTrafficLight,
      ],
      autoLoadModels: true,
      synchronize: true,
    }),
    CarModule,
    SignModule,
    TrafficLightModule,
    TicketModule,
  ],
})

export class AppModule {}