import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Ticket } from './ticket.entity';
import { TicketModule } from './ticket/ticket.module';
import { CarModule } from './car/car.module';
import { SignModule } from './sign/sign.module';
import { TrafficLightModule } from './traffic-light/traffic-light.module';

@Module({
  imports: [
    TypeOrmModule.forRoot({
      type: 'postgres',
      host: 'localhost',
      port: 5432,
      username: 'your_username',
      password: 'your_password',
      database: 'your_database_name',
      entities: [Ticket, Car, Sign, TrafficLight],
      synchronize: true,
    }),
    TicketModule,
    CarModule,
    SignModule,
    TrafficLightModule,
  ],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
