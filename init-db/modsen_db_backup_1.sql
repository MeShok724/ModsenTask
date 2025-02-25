PGDMP  6    3                }         
   ModsenTask    17.0    17.0     =           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            >           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            ?           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            @           1262    24749 
   ModsenTask    DATABASE     �   CREATE DATABASE "ModsenTask" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "ModsenTask";
                     postgres    false            �            1259    24755    Authors    TABLE     �   CREATE TABLE public."Authors" (
    "Id" uuid NOT NULL,
    "FirstName" text NOT NULL,
    "LastName" text NOT NULL,
    "DateOfBirth" timestamp with time zone NOT NULL,
    "Country" text NOT NULL
);
    DROP TABLE public."Authors";
       public         heap r       postgres    false            �            1259    24769    Books    TABLE       CREATE TABLE public."Books" (
    "Id" uuid NOT NULL,
    "ISBN" text NOT NULL,
    "Title" text NOT NULL,
    "Genre" text NOT NULL,
    "Description" text NOT NULL,
    "AuthorId" uuid NOT NULL,
    "IsTaken" boolean NOT NULL,
    "Image" bytea NOT NULL
);
    DROP TABLE public."Books";
       public         heap r       postgres    false            �            1259    24781 	   UserBooks    TABLE     �   CREATE TABLE public."UserBooks" (
    "UserId" uuid NOT NULL,
    "BookId" uuid NOT NULL,
    "TakenAt" timestamp with time zone NOT NULL,
    "ReturnBy" timestamp with time zone NOT NULL
);
    DROP TABLE public."UserBooks";
       public         heap r       postgres    false            �            1259    24762    Users    TABLE     
  CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "Email" text NOT NULL,
    "PasswordHash" text NOT NULL,
    "Role" text NOT NULL,
    "Name" text DEFAULT ''::text NOT NULL,
    "RefreshToken" text,
    "RefreshTokenExpiryTime" timestamp with time zone
);
    DROP TABLE public."Users";
       public         heap r       postgres    false            �            1259    24750    __EFMigrationsHistory    TABLE     �   CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 +   DROP TABLE public."__EFMigrationsHistory";
       public         heap r       postgres    false            7          0    24755    Authors 
   TABLE DATA           \   COPY public."Authors" ("Id", "FirstName", "LastName", "DateOfBirth", "Country") FROM stdin;
    public               postgres    false    218   >       9          0    24769    Books 
   TABLE DATA           p   COPY public."Books" ("Id", "ISBN", "Title", "Genre", "Description", "AuthorId", "IsTaken", "Image") FROM stdin;
    public               postgres    false    220   �       :          0    24781 	   UserBooks 
   TABLE DATA           P   COPY public."UserBooks" ("UserId", "BookId", "TakenAt", "ReturnBy") FROM stdin;
    public               postgres    false    221   $       8          0    24762    Users 
   TABLE DATA           z   COPY public."Users" ("Id", "Email", "PasswordHash", "Role", "Name", "RefreshToken", "RefreshTokenExpiryTime") FROM stdin;
    public               postgres    false    219   A       6          0    24750    __EFMigrationsHistory 
   TABLE DATA           R   COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    public               postgres    false    217   9       �           2606    24761    Authors PK_Authors 
   CONSTRAINT     V   ALTER TABLE ONLY public."Authors"
    ADD CONSTRAINT "PK_Authors" PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY public."Authors" DROP CONSTRAINT "PK_Authors";
       public                 postgres    false    218            �           2606    24775    Books PK_Books 
   CONSTRAINT     R   ALTER TABLE ONLY public."Books"
    ADD CONSTRAINT "PK_Books" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Books" DROP CONSTRAINT "PK_Books";
       public                 postgres    false    220            �           2606    24785    UserBooks PK_UserBooks 
   CONSTRAINT     h   ALTER TABLE ONLY public."UserBooks"
    ADD CONSTRAINT "PK_UserBooks" PRIMARY KEY ("UserId", "BookId");
 D   ALTER TABLE ONLY public."UserBooks" DROP CONSTRAINT "PK_UserBooks";
       public                 postgres    false    221    221            �           2606    24768    Users PK_Users 
   CONSTRAINT     R   ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Users" DROP CONSTRAINT "PK_Users";
       public                 postgres    false    219            �           2606    24754 .   __EFMigrationsHistory PK___EFMigrationsHistory 
   CONSTRAINT     {   ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");
 \   ALTER TABLE ONLY public."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
       public                 postgres    false    217            �           1259    24796    IX_Books_AuthorId    INDEX     M   CREATE INDEX "IX_Books_AuthorId" ON public."Books" USING btree ("AuthorId");
 '   DROP INDEX public."IX_Books_AuthorId";
       public                 postgres    false    220            �           1259    24797    IX_UserBooks_BookId    INDEX     Q   CREATE INDEX "IX_UserBooks_BookId" ON public."UserBooks" USING btree ("BookId");
 )   DROP INDEX public."IX_UserBooks_BookId";
       public                 postgres    false    221            �           2606    24776    Books FK_Books_Authors_AuthorId    FK CONSTRAINT     �   ALTER TABLE ONLY public."Books"
    ADD CONSTRAINT "FK_Books_Authors_AuthorId" FOREIGN KEY ("AuthorId") REFERENCES public."Authors"("Id") ON DELETE CASCADE;
 M   ALTER TABLE ONLY public."Books" DROP CONSTRAINT "FK_Books_Authors_AuthorId";
       public               postgres    false    220    218    4761            �           2606    24786 #   UserBooks FK_UserBooks_Books_BookId    FK CONSTRAINT     �   ALTER TABLE ONLY public."UserBooks"
    ADD CONSTRAINT "FK_UserBooks_Books_BookId" FOREIGN KEY ("BookId") REFERENCES public."Books"("Id") ON DELETE CASCADE;
 Q   ALTER TABLE ONLY public."UserBooks" DROP CONSTRAINT "FK_UserBooks_Books_BookId";
       public               postgres    false    221    220    4766            �           2606    24791 #   UserBooks FK_UserBooks_Users_UserId    FK CONSTRAINT     �   ALTER TABLE ONLY public."UserBooks"
    ADD CONSTRAINT "FK_UserBooks_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("Id") ON DELETE CASCADE;
 Q   ALTER TABLE ONLY public."UserBooks" DROP CONSTRAINT "FK_UserBooks_Users_UserId";
       public               postgres    false    221    4763    219            7   \   x��;� �NaoּA���;�,��0��;�3ѹť�ӼR�1�t�Q8�=����΋���q~�%� ��{����f(�^�m��Z��      9   j   x��;�  ����6��wa�|��R��=~�����<�!�%̺5FF���0�$�21q��������|��y=A�k����&�v؁^Z!�)�,���#���"�      :      x������ � �      8   �   x�%��n�0  �3<���01ѱQH�14��48���d�w� 􉢪�j)}�TZ����%�
RZ5�6��|�D���8hF2��<Gå圗{xi��
����~��,Q�[�YL�O��j�=��a��.,��P��<��t`�}GwJ]�~���K^'vm��7j�o��N����#�2n2Q>��.?��r�!����-^���c�ں��N�I-      6   <   x�32025046022214���L/J,���3��3�3�2�	CcCC��T>F��� T�     