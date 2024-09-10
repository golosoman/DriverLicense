import { IsString, IsOptional, IsNotEmpty, IsNumber } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';
import { ParticipantType, MovementDirectionType, SidePosition } from './RoadUsersType';

export class UpdateRoadUserDto {
  @IsString()
  @IsNotEmpty()
  @ApiProperty({
    // enum: ['passengerCar', 'walker', 'tram'],
    description: 'Тип участника движения (машина, пешеход, трамвай)',
  })
  typeParticipant: ParticipantType;

  @IsString()
  @IsNotEmpty()
  @ApiProperty({ description: 'Модель участника движения' })
  modelName: string;

  @IsString()
  @IsNotEmpty()
  @ApiProperty({
    // enum: ['west', 'east', 'north', 'south'],
    description: 'Сторона, на которой находится участник движения',
  })
  sidePosition: SidePosition;

  @IsNumber()
  @IsOptional()
  @ApiProperty({ description: 'Номер позиции на дороге, если применимо', required: false })
  numberPosition?: number;

  @IsString()
  @IsNotEmpty()
  @ApiProperty({
    // enum: ['forward', 'backward', 'left', 'right'],
    description: 'Направление движения участника',
  })
  movementDirection: MovementDirectionType;
}
