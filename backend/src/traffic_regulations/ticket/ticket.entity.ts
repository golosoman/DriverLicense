import { Entity, Column, PrimaryGeneratedColumn } from 'typeorm';

@Entity()
export class Ticket {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  type: string;

  @Column()
  question: string;

  @Column()
  correctAnswer: string;
}
