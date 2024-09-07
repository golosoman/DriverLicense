import {
  BelongsToMany,
  Column,
  DataType,
  Model,
  Table,
} from 'sequelize-typescript';
import { Ticket } from '../ticket/ticket.model';
import { TicketSign } from '../ticket/ticket_signs.model';

@Table({
  tableName: 'signs',
  timestamps: true
})
export class Sign extends Model<Sign> {
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
  //     model: () => TicketSign,
  //     unique: false,
  //   },
  //   foreignKey: 'signId',
  //   otherKey: 'ticketId',
  // })
  // tickets: Ticket[];
}
