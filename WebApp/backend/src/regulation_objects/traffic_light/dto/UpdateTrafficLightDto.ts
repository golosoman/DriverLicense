import { IsString, IsNotEmpty, IsOptional } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class UpdateTrafficLightDto {
  @IsString()
  @IsOptional()
  @ApiProperty()
  modelName: string;

  @IsString()
  @IsOptional()
  @ApiProperty()
  sidePosition: string;

  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  state: string;
}