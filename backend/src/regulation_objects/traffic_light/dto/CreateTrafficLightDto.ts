import { IsString, IsNotEmpty, IsOptional } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class CreateTrafficLightDto {
  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  sprite: string;

  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  cycle: string;
}