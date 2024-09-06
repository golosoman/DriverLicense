import { IsString, IsNotEmpty, IsOptional } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class UpdateTrafficLightDto {
    @IsString()
    @IsOptional()
    @ApiProperty()
    sprite: string;
  
    @IsString()
    @IsOptional()
    @ApiProperty()
    cycle: string;
  }