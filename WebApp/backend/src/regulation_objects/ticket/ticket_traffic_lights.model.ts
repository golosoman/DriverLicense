import {
    BelongsToMany,
    Column,
    DataType,
    Model,
    Table,
  } from 'sequelize-typescript';
  
  @Table({
    tableName: 'ticket_traffic_lights',
    timestamps: true
  })
  export class TicketTrafficLight extends Model<TicketTrafficLight> {
    @Column({
      type: DataType.INTEGER,
      allowNull: false,
    })
    ticketId: number;
  
    @Column({
      type: DataType.INTEGER,
      allowNull: false,
    })
    trafficLightId: number;

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
  