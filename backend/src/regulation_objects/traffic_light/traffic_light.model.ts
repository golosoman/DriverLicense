import {
  BelongsToMany,
  Column,
  DataType,
  Model,
  Table,
} from 'sequelize-typescript';
import { Ticket } from '../ticket/ticket.model';
import { TicketTrafficLight } from '../ticket/ticket_traffic_lights.model';

@Table({
  tableName: 'traffic_lights',
  timestamps: true
})
export class TrafficLight extends Model<TrafficLight> {
  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  sprite: string;

  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  cycle: string;

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

  // @BelongsToMany(() => Ticket, {
  //   through: {
  //     model: () => TicketTrafficLight,
  //     unique: false,
  //   },
  //   foreignKey: 'trafficLightId',
  //   otherKey: 'ticketId',
  // })
  // tickets: Ticket[];
}
