import {
  BelongsToMany,
  Column,
  DataType,
  Model,
  Table,
} from 'sequelize-typescript';
import { Ticket } from '../ticket/ticket.model';
import { TicketCar } from '../ticket/ticket_cars.model';

@Table({
  tableName: 'cars',
  timestamps: true
})
export class Car extends Model<Car> {
  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  modelName: string;

  @Column({
    type: DataType.ENUM('left', 'right', 'top', 'bottom'),
    allowNull: false,
  })
  position: string;

  @Column({
    type: DataType.INTEGER,
    allowNull: false,
  })
  speed: number;

  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  movementDirection: string;

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
  //     model: () => TicketCar,
  //     unique: false,
  //   },
  //   foreignKey: 'carId',
  //   otherKey: 'ticketId',
  // })
  // tickets: Ticket[];
}
