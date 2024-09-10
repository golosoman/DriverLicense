import {
  Column,
  DataType,
  Model,
  Table,
  BelongsToMany
} from 'sequelize-typescript';
import { RoadUser } from '../car/roadUsers.model';
import { TicketRoadUser } from './ticketRoadUser.model';
import { Sign } from '../sign/sign.model';
import { TicketSign } from './ticketSign.model';
import { TrafficLight } from '../traffic_light/traffic_light.model';
import { TicketTrafficLight } from './ticketTrafficLight.model';

@Table({
  tableName: 'tickets',
  timestamps: true
})
export class Ticket extends Model<Ticket> {
  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  typeIntersection: string;

  @Column ({
    type: DataType.STRING,
    allowNull: false,
  })
  title: string;

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

  @BelongsToMany(() => RoadUser, {
    through: {
      model: () => TicketRoadUser,
      unique: false,
    },
    foreignKey: 'ticketId',
    otherKey: 'roadUserId',
    // eager: true
  })
  roadUsersArr: RoadUser[];

  @BelongsToMany(() => Sign, {
    through: {
      model: () => TicketSign,
      unique: false,
    },
    foreignKey: 'ticketId',
    otherKey: 'signId',
  })
  signsArr: Sign[];

  @BelongsToMany(() => TrafficLight, {
    through: {
      model: () => TicketTrafficLight,
      unique: false,
    },
    foreignKey: 'ticketId',
    otherKey: 'trafficLightId',
  })
  trafficLightsArr: TrafficLight[];
}
