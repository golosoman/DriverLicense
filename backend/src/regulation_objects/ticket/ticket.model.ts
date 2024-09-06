import {
  Column,
  DataType,
  Model,
  Table,
  BelongsToMany
} from 'sequelize-typescript';
import { Car } from '../car/car.model';
import { TicketCar } from './ticket_cars.model';
import { Sign } from '../sign/sign.model';
import { TicketSign } from './ticket_signs.model';
import { TrafficLight } from '../traffic_light/traffic_light.model';
import { TicketTrafficLight } from './ticket_traffic_lights.model';

@Table({
  tableName: 'tickets',
  timestamps: true
})
export class Ticket extends Model<Ticket> {
  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  type: string;

  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  question: string;

  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  correctAnswer: string;

  @Column({
    type: DataType.DATE,
    allowNull: true
  })
  createdAt: Date;

  @Column({
    type: DataType.DATE,
    allowNull: true
  })
  updatedAt: Date;

  @BelongsToMany(() => Car, {
    through: {
      model: () => TicketCar,
      unique: false,
    },
    foreignKey: 'ticketId',
    otherKey: 'carId',
    // eager: true
  })
  cars_arr: Car[];

  @BelongsToMany(() => Sign, {
    through: {
      model: () => TicketSign,
      unique: false,
    },
    foreignKey: 'ticketId',
    otherKey: 'signId',
  })
  signs_arr: Sign[];

  @BelongsToMany(() => TrafficLight, {
    through: {
      model: () => TicketTrafficLight,
      unique: false,
    },
    foreignKey: 'ticketId',
    otherKey: 'trafficLightId',
  })
  trafficLights_arr: TrafficLight[];
}
