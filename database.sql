drop table pegawai cascade constraint purge;
drop table mobil cascade constraint purge;
drop table barang cascade constraint purge;
drop table buyer cascade constraint purge;
drop table supplier cascade constraint purge;
drop table jenis_barang cascade constraint purge;
drop table htrans_in cascade constraint purge;
drop table dtrans_in cascade constraint purge;
drop table htrans_out cascade constraint purge;
drop table dtrans_out cascade constraint purge;

create table pegawai (
	id_pegawai varchar2(6) primary key, --- substr(jabatan,1,3) + autogenerate
	nama_pegawai varchar2(20),
	jabatan varchar2(15),
	alamat_pegawai varchar2(20),
	password varchar2(15),
	nomor_telp varchar2(13)
);

create table mobil (
	id_mobil varchar2(7) primary key, --- plat nomor
	nama_mobil varchar2(15),
	kapasitas number -- 12 pakaian = 1 box
);

create table jenis_barang (
	id_jenis_barang varchar2(6) primary key, --- substr(nama_jenis_barang,1,2) + autogenerate
	nama_jenis_barang varchar2(20)
);

create table gudang (
	id_gudang varchar2(6) primary key, --- GD + autogenerate
	lokasi_gudang varchar2(20)
);

create table barang (
	id_barang varchar2(7) primary key, --- BG + autogenerate + size(S,M,L,XL)
	id_jenis_barang varchar2(6) constraint fk_idJenis references jenis_barang(id_jenis_barang),
	id_gudang varchar2(6) constraint fk_idGud references gudang(id_gudang),
	nama_barang varchar2(25),
	stock number,
	harga_jual number,
	harga_beli number
);

create table buyer (
	id_buyer varchar2(6) primary key, --- BY + autogenerate
	nama_buyer varchar2(20),
	alamat_buyer varchar2(20),
	email_buyer varchar2(20),
	jenis_buyer number --- 0: pribadi, 1: perusahaan
);

create table supplier (
	id_supplier varchar2(6) primary key, --- SP + autogenerate
	nama_supplier varchar2(20),
	alamat_supplier varchar2(20),
	email_supplier varchar2(20)
);

create table htrans_in (
	id_htrans_in varchar2(12) primary key, --- HI + DD + MM + YY + autogenerate
	id_supplier varchar2(6) constraint fk_idSupp references supplier(id_supplier),
	id_gudang varchar2(6) constraint fk_gudHin references gudang(id_gudang),
	tanggal_trans date,
	total_harga number
);

create table dtrans_in (
	id_htrans_in varchar2(12) constraint fk_Hin references htrans_in(id_htrans_in),
	id_barang varchar2(7) constraint fk_brgHin references barang(id_barang),
	stock_masuk number,
	harga_beli number,
	subtotal number,
	id_pegawai varchar(6) constraint fk_pegHin references pegawai(id_pegawai), --- pengurus
	id_supir varchar2(6) constraint fk_sopHin references pegawai(id_pegawai), --- supir
	id_mobil varchar2(7) constraint fk_mobHin references mobil(id_mobil)
);

create table htrans_out (
	id_htrans_out varchar2(12) primary key, --- HO + DD + MM + YY + autogenerate
	id_buyer varchar2(6) constraint fk_idBuy references buyer(id_buyer),
	tanggal_trans date,
	total_harga number
);

create table dtrans_out (
	id_htrans_out varchar2(12) constraint fk_Hout references htrans_out(id_htrans_out),
	id_barang varchar2(7) constraint fk_brgHout references barang(id_barang),
	stock_keluar number,
	harga_jual number,
	subtotal number,
	status number, --- 0: default, 1: cancel
	id_pegawai varchar2(6) constraint fk_pegHout references pegawai(id_pegawai), --- pengurus
	id_supir varchar2(6) constraint fk_sopHout references pegawai(id_pegawai), --- supir
	id_mobil varchar2(7) constraint fk_mobHout references mobil(id_mobil)
);

create table temp_hpp ( --- untuk history update harga
	stock_masuk number, --- stock barang masuk di htrans
	stock_awal number, --- stock awal barang di table barang
	harga_beli_baru number, --- harga beli di htrans
	harga_beli_awal number, --- harga beli di table barang
	harga_baru number --- hasil perhitungan
);

insert into pegawai values('MAN001','Lee Philpott','Manager','Ngagel Jaya 54','MAN001','03160600606');
insert into pegawai values('PEG001','Jonathan Dean','Pegawai','Darmokali V/10','PEG001','081323242089');
insert into pegawai values('PEG002','Chris Greenhill','Pegawai','Ketintang II/35','PEG002','08793287610');
insert into pegawai values('PEG003','Jonny Hughes','Pegawai','Jetis Wetan 97','PEG003','03170070770');
insert into pegawai values('SUP001','Graham Duffield','Supir','Dukuh Pakis X/76','SUP001','0317325638');
insert into pegawai values('SUP002','Magno Vieira','Supir','Raya Darmo 59','SUP001','088880829101');
insert into pegawai values('ADMIN','Admin','Admin','-','admin','-');

insert into mobil values('L8906XE','Hi Max',20);
insert into mobil values('W7549YX','L300',15);
insert into mobil values('S1236OP','Carry Pickup',10);
insert into mobil values('L8932IL','Grand Max',18);

insert into jenis_barang values('KE0001','Kemeja');
insert into jenis_barang values('KA0001','Kaos');
insert into jenis_barang values('CP0001','Celana Panjang');
insert into jenis_barang values('CP0002','Celana Pendek');
insert into jenis_barang values('KP0001','Kemeja Panjang');

insert into gudang values('GD0001','Basuki Rahmat 98');
insert into gudang values('GD0002','Keputran 123');
insert into gudang values('GD0003','A Yani 67');

insert into barang values('BG0001S','KE0001','GD0002','Kemeja Kotak Merah (S)',10,70000,85000);
insert into barang values('BG0001M','KA0001','GD0001','Kaos Merah (M)',15,60000,75000);
insert into barang values('BG001XL','KP0001','GD0002','Kemeja Panjang Biru (XL)',20,85000,95000);
insert into barang values('BG0002S','CP0002','GD0002','Celana Pendek Jeans (S)',15,75000,90000);
insert into barang values('BG0001L','KE0001','GD0003','Kemeja Kotak Hijau (L)',15,70000,85000);
insert into barang values('BG0001M','CP0001','GD0002','Celana Panjang Jeans (M)',10,80000,95000);

insert into buyer values('BY0001','David Bosumtwe','Ngagel Tengah IV/9','david1@gmail.com',1);
insert into buyer values('BY0002','Adam Bouskouchi','Kupang Krajan V/2','adam1@gmail.com',0);
insert into buyer values('BY0003','David Currie','Barata Jaya III/18','david2@gmail.com',0);
insert into buyer values('BY0004','Curtis Dawes','Ngagel Jaya 53','curtis1@gmail.com',1);
insert into buyer values('BY0005','Frederik De Jong','Gayungsari VII/7','frederik1@gmail.com',0);
insert into buyer values('BY0006','Joseph Chenwi','Ketintang Barat V/19','joseph1@gmail.com',1);

insert into supplier values('SP0001','Peter Bint','Siwalankerto X/20','peter1@gmail.com');
insert into supplier values('SP0002','Lionel Foy','Semolowaru Barat 53','lionel1@gmail.com');
insert into supplier values('SP0003','Mark Gibbon','Nginden Utara II/83','mark1@gmail.com');
insert into supplier values('SP0004','Diomansy Kamara','Dharmawangsa 74','diomansy1@gmail.com');
insert into supplier values('SP0005','Olusola Omole','Dharmahusada 19','olusola1@gmail.com');