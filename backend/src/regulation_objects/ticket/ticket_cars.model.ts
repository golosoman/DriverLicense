import {
  BelongsToMany,
  Column,
  DataType,
  Model,
  Table,
} from 'sequelize-typescript';

@Table({
  tableName: 'ticket_cars',
  timestamps: true
})
export class TicketCar extends Model<TicketCar> {
  @Column({
    type: DataType.INTEGER,
    allowNull: false,
  })
  ticketId: number;

  @Column({
    type: DataType.INTEGER,
    allowNull: false,
  })
  carId: number;

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
}
